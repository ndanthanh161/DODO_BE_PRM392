namespace SMEFLOWSystem.Application.DTOs.AuthDtos;

/// <summary>Danh tính đã được backend xác minh từ Firebase ID token.</summary>
public sealed record FirebaseIdentityDto(string Uid, string Email);
