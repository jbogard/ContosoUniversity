// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRegistry.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using StructureMap;

namespace ContosoUniversity.DependencyResolution {
    using System.Web.Mvc;
    using DAL;
    using FluentValidation;
    using FluentValidation.Mvc;
    using Infrastructure;
    using Infrastructure.Validation;
    using StructureMap.Configuration.DSL;
    using StructureMap.Graph;
    using StructureMap.Pipeline;

    public class DefaultRegistry : Registry {
        #region Constructors and Destructors

        public DefaultRegistry() {
            Scan(
                scan => {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                    scan.LookForRegistries();
                    scan.AssemblyContainingType<DefaultRegistry>();
                    scan.AddAllTypesOf(typeof(IModelBinder));
                    scan.AddAllTypesOf(typeof(IModelBinderProvider));
                    scan.With(new ControllerConvention());
                });
            For<SchoolContext>().Use(() => new SchoolContext("SchoolContext")).LifecycleIs<TransientLifecycle>();
            For<IControllerFactory>().Use<ControllerFactory>();
            For<ModelValidatorProvider>().Use<FluentValidationModelValidatorProvider>();
            For<IValidatorFactory>().Use<StructureMapValidatorFactory>();
        }

        #endregion
    }
}