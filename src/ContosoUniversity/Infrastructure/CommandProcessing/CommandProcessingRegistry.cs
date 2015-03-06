namespace ContosoUniversity.Infrastructure.CommandProcessing
{
    using FluentValidation;
    using MediatR;
    using StructureMap.Configuration.DSL;
    using StructureMap.Graph;
    using Validation;

    public class CommandProcessingRegistry : Registry
    {
        public CommandProcessingRegistry()
        {
            Scan(scan =>
            {
                scan.AssemblyContainingType<IMediator>();
                scan.AssemblyContainingType<CommandProcessingRegistry>();

                scan.AddAllTypesOf(typeof(IRequestHandler<,>));
                scan.AddAllTypesOf(typeof(IAsyncRequestHandler<,>));
                scan.AddAllTypesOf(typeof (IValidator<>));
                scan.WithDefaultConventions();
            });

            For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => t => ctx.GetInstance(t));
            For<MultiInstanceFactory>().Use<MultiInstanceFactory>(ctx => t => ctx.GetAllInstances(t));
            For(typeof(IRequestHandler<,>)).DecorateAllWith(typeof(MediatorPipeline<,>));
            For(typeof(RequestHandler<>)).DecorateAllWith(typeof(CommandPipeline<>));
        }
    }
}