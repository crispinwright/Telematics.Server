using System;
using Ninject;

namespace Telematics.Server.NinjectUtils
{
    public static class Kernel
    {
        private static IKernel _instance;
        public static IKernel Instance
        {
            get
            {
                return _instance;
            }
            set
            {
                if (_instance !=  null)
                    throw new InvalidOperationException("Cannot set kernel twice.");
                _instance = value;
            }
        }
        
    }
}