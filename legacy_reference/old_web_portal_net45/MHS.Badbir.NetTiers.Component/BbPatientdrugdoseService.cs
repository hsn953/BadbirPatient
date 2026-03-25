
#region Using Directives
using System;
using System.ComponentModel;
using System.Collections;
using System.Xml.Serialization;
using System.Data;

using MHS.Badbir.NetTiers.Entities;
using MHS.Badbir.NetTiers.Entities.Validation;

using MHS.Badbir.NetTiers.Data;
using Microsoft.Practices.EnterpriseLibrary.Logging;

#endregion

namespace MHS.Badbir.NetTiers.Component
{		
	/// <summary>
	/// An component type implementation of the 'bbPatientdrugdose' table.
	/// </summary>
	/// <remarks>
	/// All custom implementations should be done here.
	/// </remarks>
	[CLSCompliant(true)]
	public partial class BbPatientdrugdoseService : MHS.Badbir.NetTiers.Component.BbPatientdrugdoseServiceBase
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the BbPatientdrugdoseService class.
		/// </summary>
		public BbPatientdrugdoseService() : base()
		{
		}
		#endregion Constructors
		
	}//End Class

} // end namespace
