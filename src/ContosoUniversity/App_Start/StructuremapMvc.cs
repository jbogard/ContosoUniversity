// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StructuremapMvc.cs" company="Web Advanced">
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

using ContosoUniversity.App_Start;

using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(StructuremapMvc), "Start")]
[assembly: ApplicationShutdownMethod(typeof(StructuremapMvc), "End")]

namespace ContosoUniversity.App_Start {
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

	using ContosoUniversity.DependencyResolution;
	using Infrastructure.Mapping;
	using StructureMap;

    public static class StructuremapMvc
    {
        public static StructureMapDependencyScope ParentScope { get; set; }

        public static void End()
        {
            ParentScope.Dispose();
            ParentScope.DisposeParentContainer();
        }

        public static void Start()
        {
            IContainer container = IoC.Initialize();
            ParentScope = new StructureMapDependencyScope(container, new HttpContextNestedContainerScope());
            DependencyResolver.SetResolver(ParentScope);
            DynamicModuleUtility.RegisterModule(typeof(StructureMapScopeModule));
        }
    }

    public class HttpContextNestedContainerScope : INestedContainerScope
    {
        private const string NestedContainerKey = "Nested.Container.Key";

        public IContainer NestedContainer
        {
            get { return (IContainer)(HttpContext.Current != null ? HttpContext.Current.Items[NestedContainerKey] : null); }
            set { HttpContext.Current.Items[NestedContainerKey] = value; }
        }
    }

}