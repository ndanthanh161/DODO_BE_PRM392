using System;

namespace SMEFLOWSystem.SharedKernel.Interfaces;

public interface ICurrentUserService
{
    Guid? UserId { get; }
    bool IsInRole(string role);
}
