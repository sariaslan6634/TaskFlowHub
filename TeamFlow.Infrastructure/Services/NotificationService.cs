// Infrastructure/Services/NotificationService.cs
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using TeamFlow.Application.DTOs.Notification;
using TeamFlow.Application.Interfaces;
using TeamFlow.Application.Interfaces.Services;
using TeamFlow.Domain.Entities;

namespace TeamFlow.Infrastructure.Services;

public class NotificationService : INotificationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly INotificationHubService _hubService;

    public NotificationService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        INotificationHubService hubService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _hubService = hubService;
    }

    public async Task<IEnumerable<NotificationResponseDto>> GetUnreadAsync(int userId)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(userId);
        if (user == null)
            throw new KeyNotFoundException($"Kullanıcı bulunamadı. Id: {userId}");

        var notifications = await _unitOfWork.Notifications.GetUnreadByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<NotificationResponseDto>>(notifications);
    }

    public async Task MarkAllAsReadAsync(int userId)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(userId);
        if (user == null)
            throw new KeyNotFoundException($"Kullanıcı bulunamadı. Id: {userId}");

        await _unitOfWork.Notifications.MarkAllAsReadAsync(userId);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task CreateAsync(CreateNotificationDto request)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(request.UserId);
        if (user == null)
            throw new KeyNotFoundException($"Kullanıcı bulunamadı. Id: {request.UserId}");

        var notification = _mapper.Map<Notification>(request);
        await _unitOfWork.Notifications.AddAsync(notification);
        await _unitOfWork.SaveChangesAsync();

        // Anlık bildirim gönder
        await _hubService.SendNotificationAsync(request.UserId, new
        {
            notification.Title,
            notification.Message,
            notification.Type,
            CreatedAt = DateTime.UtcNow
        });
    }
}