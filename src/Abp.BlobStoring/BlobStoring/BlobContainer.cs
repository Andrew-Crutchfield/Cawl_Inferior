﻿using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Abp.Runtime.Session;
using Abp.Threading;

namespace Abp.BlobStoring
{
    public class BlobContainer<TContainer> : IBlobContainer<TContainer>
        where TContainer : class
    {
        protected readonly IBlobContainer Container;

        public BlobContainer(IBlobContainerFactory blobContainerFactory)
        {
            Container = blobContainerFactory.Create<TContainer>();
        }

        public virtual Task SaveAsync(
            string name,
            Stream stream,
            bool overrideExisting = false,
            CancellationToken cancellationToken = default)
        {
            return Container.SaveAsync(
                name,
                stream,
                overrideExisting,
                cancellationToken
            );
        }

        public virtual Task<bool> DeleteAsync(
            string name,
            CancellationToken cancellationToken = default)
        {
            return Container.DeleteAsync(
                name,
                cancellationToken
            );
        }

        public virtual Task<bool> ExistsAsync(
            string name,
            CancellationToken cancellationToken = default)
        {
            return Container.ExistsAsync(
                name,
                cancellationToken
            );
        }

        public virtual Task<Stream> GetAsync(
            string name,
            CancellationToken cancellationToken = default)
        {
            return Container.GetAsync(
                name,
                cancellationToken
            );
        }

        public virtual Task<Stream> GetOrNullAsync(
            string name,
            CancellationToken cancellationToken = default)
        {
            return Container.GetOrNullAsync(
                name,
                cancellationToken
            );
        }
    }

    public class BlobContainer : IBlobContainer
    {
        protected string ContainerName { get; }

        protected BlobContainerConfiguration Configuration { get; }

        protected IBlobProvider Provider { get; }

        protected IAbpSession CurrentTenant { get; }

        protected ICancellationTokenProvider CancellationTokenProvider { get; }

        protected IServiceProvider ServiceProvider { get; }

        protected IBlobNormalizeNamingService BlobNormalizeNamingService { get; }

        public BlobContainer(
            string containerName,
            BlobContainerConfiguration configuration,
            IBlobProvider provider,
            IAbpSession currentTenant,
            ICancellationTokenProvider cancellationTokenProvider,
            IBlobNormalizeNamingService blobNormalizeNamingService,
            IServiceProvider serviceProvider)
        {
            ContainerName = containerName;
            Configuration = configuration;
            Provider = provider;
            CurrentTenant = currentTenant;
            CancellationTokenProvider = cancellationTokenProvider;
            BlobNormalizeNamingService = blobNormalizeNamingService;
            ServiceProvider = serviceProvider;
        }

        public virtual async Task SaveAsync(
            string name,
            Stream stream,
            bool overrideExisting = false,
            CancellationToken cancellationToken = default)
        {
            using (CurrentTenant.Use(GetTenantIdOrNull(), null))
            {
                var blobNormalizeNaming = BlobNormalizeNamingService.NormalizeNaming(Configuration, ContainerName, name);

                await Provider.SaveAsync(
                    new BlobProviderSaveArgs(
                        blobNormalizeNaming.ContainerName,
                        Configuration,
                        blobNormalizeNaming.BlobName,
                        stream,
                        overrideExisting,
                        CancellationTokenProvider.FallbackToProvider(cancellationToken)
                    )
                );
            }
        }

        public virtual async Task<bool> DeleteAsync(
            string name,
            CancellationToken cancellationToken = default)
        {
            using (CurrentTenant.Use(GetTenantIdOrNull(), null))
            {
                var blobNormalizeNaming =
                    BlobNormalizeNamingService.NormalizeNaming(Configuration, ContainerName, name);

                return await Provider.DeleteAsync(
                    new BlobProviderDeleteArgs(
                        blobNormalizeNaming.ContainerName,
                        Configuration,
                        blobNormalizeNaming.BlobName,
                        CancellationTokenProvider.FallbackToProvider(cancellationToken)
                    )
                );
            }
        }

        public virtual async Task<bool> ExistsAsync(
            string name,
            CancellationToken cancellationToken = default)
        {
            using (CurrentTenant.Use(GetTenantIdOrNull(), null))
            {
                var blobNormalizeNaming =
                    BlobNormalizeNamingService.NormalizeNaming(Configuration, ContainerName, name);

                return await Provider.ExistsAsync(
                    new BlobProviderExistsArgs(
                        blobNormalizeNaming.ContainerName,
                        Configuration,
                        blobNormalizeNaming.BlobName,
                        CancellationTokenProvider.FallbackToProvider(cancellationToken)
                    )
                );
            }
        }

        public virtual async Task<Stream> GetAsync(
            string name,
            CancellationToken cancellationToken = default)
        {
            var stream = await GetOrNullAsync(name, cancellationToken);

            if (stream == null)
            {
                //TODO: Consider to throw some type of "not found" exception and handle on the HTTP status side
                throw new AbpException(
                    $"Could not found the requested BLOB '{name}' in the container '{ContainerName}'!");
            }

            return stream;
        }

        public virtual async Task<Stream> GetOrNullAsync(
            string name,
            CancellationToken cancellationToken = default)
        {
            using (CurrentTenant.Use(GetTenantIdOrNull(), null))
            {
                var blobNormalizeNaming =
                    BlobNormalizeNamingService.NormalizeNaming(Configuration, ContainerName, name);

                return await Provider.GetOrNullAsync(
                    new BlobProviderGetArgs(
                        blobNormalizeNaming.ContainerName,
                        Configuration,
                        blobNormalizeNaming.BlobName,
                        CancellationTokenProvider.FallbackToProvider(cancellationToken)
                    )
                );
            }
        }

        protected virtual int? GetTenantIdOrNull()
        {
            if (!Configuration.IsMultiTenant)
            {
                return null;
            }

            return CurrentTenant.TenantId;
        }
    }

}
