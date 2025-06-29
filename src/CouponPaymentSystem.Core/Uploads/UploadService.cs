using System;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.BlobStoring;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using AutoInterfaceAttributes;
using CouponPaymentSystem.Core.Data.Entities;
using JetBrains.Annotations;

namespace CouponPaymentSystem.Core.Uploads;

[AutoInterface(Inheritance = [typeof(IApplicationService)])]
[UsedImplicitly]
public class UploadService : ApplicationService, IUploadService
{
    private readonly IRepository<Upload, Guid> _uploadRepository;
    private readonly IBlobContainer _blobContainer;

    public UploadService(IRepository<Upload, Guid> uploadRepository, IBlobContainer blobContainer)
    {
        _uploadRepository = uploadRepository;
        _blobContainer = blobContainer;
    }

    public async Task DebugUploadAsync()
    {
        var bytes = Encoding.UTF8.GetBytes($"{GuidPolyfill.CreateVersion7()}");
        await _blobContainer.SaveAsync(GuidPolyfill.CreateVersion7().ToString(), bytes, true);
        Logger.Info("Debug upload");
    }

    public async Task<Guid> UploadAsync(UploadRequestDto requestDto)
    {
        if (await _blobContainer.ExistsAsync(requestDto.Name))
        {
            var existingUpload = await _uploadRepository.SingleAsync(s =>
                s.FileName == requestDto.Name
            );
            return existingUpload.Id;
        }

        await _blobContainer.SaveAsync(requestDto.Name, requestDto.Content);
        var newUpload = requestDto.Map();
        return await _uploadRepository.InsertAndGetIdAsync(newUpload);
    }
}
