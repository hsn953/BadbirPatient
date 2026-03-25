#region Using directives

using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;

using System.Diagnostics;
using MHS.Badbir.NetTiers.Entities;
using MHS.Badbir.NetTiers.Data;

#endregion

namespace MHS.Badbir.NetTiers.Data.Bases
{	
	///<summary>
	/// This class is the base class for any <see cref="BbPappPatientMedProblemFupProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class BbPappPatientMedProblemFupProviderBase : BbPappPatientMedProblemFupProviderBaseCore
	{
	} // end class
} // end namespace
