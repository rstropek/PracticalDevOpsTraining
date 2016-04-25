using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Web.Http.Dependencies;

namespace Books
{
    /// <summary>
    /// Implements a dependency injection container based on MEF.
    /// </summary>
    public class MefDependencyResolver : IDependencyResolver
    {
        private CompositionContainer container;

        /// <summary>
        /// Initializes a new instance of the <see cref="MefDependencyResolver"/> class.
        /// </summary>
        /// <param name="container">MEF composition container used for DI</param>
        public MefDependencyResolver(CompositionContainer container)
        {
            this.container = container;
        }

		/// <summary>
		/// Finalizer for <see cref="MefDependencyResolver"/>
		/// </summary>
		~MefDependencyResolver()
		{
			this.Dispose(false);
		}

        /// <inheritdoc />
        public IDependencyScope BeginScope()
        {
            return new MefDependencyResolver(new CompositionContainer(this.container.Catalog, this.container));
        }

        /// <inheritdoc />
        public void Dispose()
        {
			this.Dispose(true);
        }

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <param name="disposing">Value indicating whether this method was explicitly called</param>
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.container.Dispose();
				GC.SuppressFinalize(this);
			}
		}

        /// <inheritdoc />
        public object GetService(Type serviceType)
        {
            var exports = this.container.GetExports(serviceType, null, null).FirstOrDefault();
            return exports?.Value;
        }

        /// <inheritdoc />
        public IEnumerable<object> GetServices(Type serviceType)
        {
            var exports = this.container.GetExports(serviceType, null, null);
            var result = new List<object>(exports.Count());
            foreach(var export in exports)
            {
                result.Add(export.Value);
            }

            return result;
        }
    }
}