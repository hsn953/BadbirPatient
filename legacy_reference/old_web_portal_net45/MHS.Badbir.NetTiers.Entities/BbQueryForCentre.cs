#region Using directives

using System;

#endregion

namespace MHS.Badbir.NetTiers.Entities
{	
	///<summary>
	/// This table stores all queries, their related flags, subject and linked ids to other tables. Adverse Events are linked by a many to many link in a separate Link table.	
	///</summary>
	/// <remarks>
	/// This file is generated once and will never be overwritten.
	/// </remarks>	
	[Serializable]
	[CLSCompliant(true)]
	public partial class BbQueryForCentre : BbQueryForCentreBase
	{		
		#region Constructors

		///<summary>
		/// Creates a new <see cref="BbQueryForCentre"/> instance.
		///</summary>
		public BbQueryForCentre():base(){}	
		
		#endregion
	}
}
