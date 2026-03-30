#region Using directives

using System;

#endregion

namespace MHS.Badbir.NetTiers.Entities
{	
	///<summary>
	/// An object representation of the 'bbAdditionalUserDetail' table. [No description found the database]	
	///</summary>
	/// <remarks>
	/// This file is generated once and will never be overwritten.
	/// </remarks>	
	[Serializable]
	[CLSCompliant(true)]
	public partial class BbAdditionalUserDetail : BbAdditionalUserDetailBase
	{		
		#region Constructors

		///<summary>
		/// Creates a new <see cref="BbAdditionalUserDetail"/> instance.
		///</summary>
		public BbAdditionalUserDetail():base(){}

        #endregion

        ///<summary>
        /// Fullname is a read-only property of BbAdditionalUserDetail that combines FName and LName (forename and surname)
        ///</summary>
        public string Fullname
        {
            get { return string.Format("{0} {1}", this.FName, this.LName); }
        }
	}
}
