using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        // 1. MEDIATR
        // Isso olha pra assembly, acha todas as classes q implementa IRequestHandler<T,R>,
        // e registra aqui, asScoped.
        services.AddMediatR(config =>
            config.RegisterServicesFromAssembly(typeof(DependencyInjectionExtension).Assembly));

        // 2. FUTURE: FLUENT VALIDATION
        // se no futuro quiser add, é tipo assim
        // services.AddValidatorsFromAssembly(typeof(DependencyInjectionExtension).Assembly);
    }
}