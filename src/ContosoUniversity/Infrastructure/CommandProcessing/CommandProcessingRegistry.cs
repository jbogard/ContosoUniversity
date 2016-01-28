namespace ContosoUniversity.Infrastructure.CommandProcessing
{
    using FluentValidation;
    using MediatR;
    using StructureMap;

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
        }
    }
}