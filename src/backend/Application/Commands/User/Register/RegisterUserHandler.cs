using Domain.Errors;
using Domain.Primitives;
using Domain.Repositories;
using Domain.Repositories.Users;
using Domain.ValueObjects;
using MediatR;

namespace Application.Commands.User.Register;
                                            
public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Result<RegisterUserResponse>>
{
    private readonly IUserReadOnlyRepository _readRepo;
    private readonly IUserWriteOnlyRepository _writeRepo;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserHandler(
        IUserReadOnlyRepository readRepo,
        IUserWriteOnlyRepository writeRepo,
        IUnitOfWork unitOfWork)
    {
        _readRepo = readRepo;
        _writeRepo = writeRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RegisterUserResponse>> Handle(RegisterUserCommand request, CancellationToken ct)
    {
        // VOs, (eles jogam quando inválido, o handler n trata, o middleware trata e devolve 400/500)
        // alternativamente, vc pode fazer try/catch aqui e devolver Result.Failure
        var email = Email.Create(request.Email);
        var password = PasswordHash.CreateFromRaw(request.Password);
        var cpf = Cpf.Create(request.Cpf);

        // 2. checa se o email já existe
        var emailExists = await _readRepo.ExistsByEmailAsync(email, ct);
        if (emailExists)
        {
            return Result<RegisterUserResponse>.Failure(UserErrors.AlreadyExists);
        }

        // Create Entity
        var user = Domain.Entities.User.Create(
            request.FirstName,
            request.LastName,
            email,
            password,
            cpf
        );

        await _writeRepo.AddAsync(user, ct);

        await _unitOfWork.CommitAsync(ct);

        // retorna pattern matching
        return Result<RegisterUserResponse>.Success(new RegisterUserResponse(user.UserIdentifier));
    }
}