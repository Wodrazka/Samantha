using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Samantha
{

    public enum Scope
    {
        Instance,
        PerRequest
    }

    public class RegistrationSettings
    {

        #region Fields

        #endregion

        #region Properties
        
        public Scope Scope { get; set; }

        public bool IsGeneric { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public RegistrationSettings()
        {

        }

        #endregion

        #region Methods

        #endregion

    }
}
