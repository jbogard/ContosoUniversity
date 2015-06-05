// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StructureMapDependencyScope.cs" company="Web Advanced">
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

namespace ContosoUniversity.DependencyResolution {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using Microsoft.Practices.ServiceLocation;

    using StructureMap;

    /// <summary>
    /// The structure map dependency scope.
    /// </summary>
    public class StructureMapDependencyScope : ServiceLocatorImplBase
    {
        private readonly INestedContainerScope _nestedContainerScope;

        /// <summary>
        /// Initializes the scope using a <see cref="TransientNestedContainerScope"/> container)
        /// </summary>
        public StructureMapDependencyScope(IContainer container)
            : this(container, new TransientNestedContainerScope())
        {

        }

        public StructureMapDependencyScope(IContainer container, INestedContainerScope nestedContainerScope)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            Container = container;
            _nestedContainerScope = nestedContainerScope;
        }


        public IContainer Container { get; set; }

        public IContainer CurrentNestedContainer
        {
            get { return _nestedContainerScope.NestedContainer; }
            set { _nestedContainerScope.NestedContainer = value; }
        }

        public void CreateNestedContainer()
        {
            if (CurrentNestedContainer != null)
            {
                return;
            }

            CurrentNestedContainer = Container.GetNestedContainer();
        }

        public void Dispose()
        {
            DisposeNestedContainer();
        }

        public void DisposeNestedContainer()
        {
            if (CurrentNestedContainer != null)
            {
                CurrentNestedContainer.Dispose();
                CurrentNestedContainer = null;
            }
        }

        public void DisposeParentContainer()
        {
            if (Container != null)
            {
                Container.Dispose();
                Container = null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return DoGetAllInstances(serviceType);
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return (CurrentNestedContainer ?? Container).GetAllInstances(serviceType).Cast<object>();
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            IContainer container = (CurrentNestedContainer ?? Container);

            if (string.IsNullOrEmpty(key))
            {
                return serviceType.IsAbstract || serviceType.IsInterface
                    ? container.TryGetInstance(serviceType)
                    : container.GetInstance(serviceType);
            }

            return container.GetInstance(serviceType, key);
        }

        public object TryGetInstance(Type type)
        {
            IContainer container = (CurrentNestedContainer ?? Container);
            return container.TryGetInstance(type);
        }
    }

    public interface INestedContainerScope
    {
        IContainer NestedContainer { get; set; }
    }

    public class TransientNestedContainerScope : INestedContainerScope
    {
        public IContainer NestedContainer { get; set; }
    }
}
