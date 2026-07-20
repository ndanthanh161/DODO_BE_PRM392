using SMEFLOWSystem.Application.DTOs.AuthDtos;

namespace SMEFLOWSystem.Application.Interfaces.IServices;

public interface IFirebaseTokenVerifier
{
    Task<FirebaseIdentityDto> VerifyAsync(string idToken);
}
