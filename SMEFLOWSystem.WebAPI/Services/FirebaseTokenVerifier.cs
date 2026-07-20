using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using SMEFLOWSystem.Application.DTOs.AuthDtos;
using SMEFLOWSystem.Application.Interfaces.IServices;

namespace SMEFLOWSystem.WebAPI.Services;

public sealed class FirebaseTokenVerifier : IFirebaseTokenVerifier
{
    private static readonly object AppLock = new();
    private static FirebaseApp? _firebaseApp;

    private readonly string _projectId;

    public FirebaseTokenVerifier(IConfiguration configuration)
    {
        _projectId = configuration["Firebase:ProjectId"]?.Trim()
            ?? throw new InvalidOperationException("Firebase:ProjectId chưa được cấu hình.");

        if (string.IsNullOrWhiteSpace(_projectId))
            throw new InvalidOperationException("Firebase:ProjectId chưa được cấu hình.");
    }

    public async Task<FirebaseIdentityDto> VerifyAsync(string idToken)
    {
        if (string.IsNullOrWhiteSpace(idToken))
            throw new UnauthorizedAccessException("Thiếu Firebase ID token.");

        try
        {
            var decoded = await GetFirebaseAuth().VerifyIdTokenAsync(
                idToken.Trim(),
                checkRevoked: true);

            if (!decoded.Claims.TryGetValue("email", out var emailClaim) ||
                string.IsNullOrWhiteSpace(emailClaim?.ToString()))
            {
                throw new UnauthorizedAccessException(
                    "Firebase token không chứa email hợp lệ.");
            }

            return new FirebaseIdentityDto(
                decoded.Uid,
                emailClaim!.ToString()!.Trim().ToLowerInvariant());
        }
        catch (FirebaseAuthException ex)
        {
            throw new UnauthorizedAccessException(
                "Firebase token không hợp lệ, đã hết hạn hoặc đã bị thu hồi.",
                ex);
        }
    }

    private FirebaseAuth GetFirebaseAuth()
    {
        if (_firebaseApp != null)
            return FirebaseAuth.GetAuth(_firebaseApp);

        lock (AppLock)
        {
            _firebaseApp ??= FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.GetApplicationDefault(),
                ProjectId = _projectId
            });
        }

        return FirebaseAuth.GetAuth(_firebaseApp);
    }
}
