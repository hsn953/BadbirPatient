#region Using directives

using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Configuration.Provider;

using MHS.Badbir.NetTiers.Entities;

#endregion

namespace MHS.Badbir.NetTiers.Data.Bases
{	
	///<summary>
	/// The base class to implements to create a .NetTiers provider.
	///</summary>
	public abstract class NetTiersProvider : NetTiersProviderBase
	{
		
		///<summary>
		/// Current BbPatientCohortTrackingStatuslkpProviderBase instance.
		///</summary>
		public virtual BbPatientCohortTrackingStatuslkpProviderBase BbPatientCohortTrackingStatuslkpProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BbPatientProviderBase instance.
		///</summary>
		public virtual BbPatientProviderBase BbPatientProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BbCentreRegionlkpProviderBase instance.
		///</summary>
		public virtual BbCentreRegionlkpProviderBase BbCentreRegionlkpProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BbCentrestatusProviderBase instance.
		///</summary>
		public virtual BbCentrestatusProviderBase BbCentrestatusProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BbUkcrNregionlkpProviderBase instance.
		///</summary>
		public virtual BbUkcrNregionlkpProviderBase BbUkcrNregionlkpProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BbCentreProviderBase instance.
		///</summary>
		public virtual BbCentreProviderBase BbCentreProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BbPatientdrugProviderBase instance.
		///</summary>
		public virtual BbPatientdrugProviderBase BbPatientdrugProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BbQueryTypelkpProviderBase instance.
		///</summary>
		public virtual BbQueryTypelkpProviderBase BbQueryTypelkpProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BbQueryStatuslkpProviderBase instance.
		///</summary>
		public virtual BbQueryStatuslkpProviderBase BbQueryStatuslkpProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BbPatientStatusDetaillkpProviderBase instance.
		///</summary>
		public virtual BbPatientStatusDetaillkpProviderBase BbPatientStatusDetaillkpProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BbPatientdrugdoseProviderBase instance.
		///</summary>
		public virtual BbPatientdrugdoseProviderBase BbPatientdrugdoseProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BbPatientLifestyleProviderBase instance.
		///</summary>
		public virtual BbPatientLifestyleProviderBase BbPatientLifestyleProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BbQueryForCentreProviderBase instance.
		///</summary>
		public virtual BbQueryForCentreProviderBase BbQueryForCentreProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BbPatientStatuslkpProviderBase instance.
		///</summary>
		public virtual BbPatientStatuslkpProviderBase BbPatientStatuslkpProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BbQueryForCentreMessageProviderBase instance.
		///</summary>
		public virtual BbQueryForCentreMessageProviderBase BbQueryForCentreMessageProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BbGenderlkpProviderBase instance.
		///</summary>
		public virtual BbGenderlkpProviderBase BbGenderlkpProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BbPatientCohortHistoryProviderBase instance.
		///</summary>
		public virtual BbPatientCohortHistoryProviderBase BbPatientCohortHistoryProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BbQueryProviderBase instance.
		///</summary>
		public virtual BbQueryProviderBase BbQueryProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BbQueryMessageProviderBase instance.
		///</summary>
		public virtual BbQueryMessageProviderBase BbQueryMessageProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BbSaeClinicianlkpProviderBase instance.
		///</summary>
		public virtual BbSaeClinicianlkpProviderBase BbSaeClinicianlkpProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BbCohortlkpProviderBase instance.
		///</summary>
		public virtual BbCohortlkpProviderBase BbCohortlkpProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BbTitlelkpProviderBase instance.
		///</summary>
		public virtual BbTitlelkpProviderBase BbTitlelkpProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BbPositionRolelkpProviderBase instance.
		///</summary>
		public virtual BbPositionRolelkpProviderBase BbPositionRolelkpProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BbWorkStatuslkpProviderBase instance.
		///</summary>
		public virtual BbWorkStatuslkpProviderBase BbWorkStatuslkpProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BbPappPatientMedProblemFupProviderBase instance.
		///</summary>
		public virtual BbPappPatientMedProblemFupProviderBase BbPappPatientMedProblemFupProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BbAdditionalUserDetailProviderBase instance.
		///</summary>
		public virtual BbAdditionalUserDetailProviderBase BbAdditionalUserDetailProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BbAnswerlkpProviderBase instance.
		///</summary>
		public virtual BbAnswerlkpProviderBase BbAnswerlkpProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BbCommonFrequencylkpProviderBase instance.
		///</summary>
		public virtual BbCommonFrequencylkpProviderBase BbCommonFrequencylkpProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BbConfigFactoryProviderBase instance.
		///</summary>
		public virtual BbConfigFactoryProviderBase BbConfigFactoryProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BbEthnicitylkpProviderBase instance.
		///</summary>
		public virtual BbEthnicitylkpProviderBase BbEthnicitylkpProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BbFileStorageProviderBase instance.
		///</summary>
		public virtual BbFileStorageProviderBase BbFileStorageProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BbLesionlkpProviderBase instance.
		///</summary>
		public virtual BbLesionlkpProviderBase BbLesionlkpProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BbPatientCohortTrackingClinicAttendanceLkpProviderBase instance.
		///</summary>
		public virtual BbPatientCohortTrackingClinicAttendanceLkpProviderBase BbPatientCohortTrackingClinicAttendanceLkpProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BbLoginLogProviderBase instance.
		///</summary>
		public virtual BbLoginLogProviderBase BbLoginLogProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BbMailingListSubscriptionsProviderBase instance.
		///</summary>
		public virtual BbMailingListSubscriptionsProviderBase BbMailingListSubscriptionsProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BbNotificationTypelkpProviderBase instance.
		///</summary>
		public virtual BbNotificationTypelkpProviderBase BbNotificationTypelkpProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BbNotificationProviderBase instance.
		///</summary>
		public virtual BbNotificationProviderBase BbNotificationProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BbPappPatientCohortTrackingProviderBase instance.
		///</summary>
		public virtual BbPappPatientCohortTrackingProviderBase BbPappPatientCohortTrackingProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BbPappPatientDlqiProviderBase instance.
		///</summary>
		public virtual BbPappPatientDlqiProviderBase BbPappPatientDlqiProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BbPappPatientLifestyleProviderBase instance.
		///</summary>
		public virtual BbPappPatientLifestyleProviderBase BbPappPatientLifestyleProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BbMailingListsProviderBase instance.
		///</summary>
		public virtual BbMailingListsProviderBase BbMailingListsProvider{get {throw new NotImplementedException();}}
		
		///<summary>
		/// Current BbPatientCohortTrackingProviderBase instance.
		///</summary>
		public virtual BbPatientCohortTrackingProviderBase BbPatientCohortTrackingProvider{get {throw new NotImplementedException();}}
		
		
	}
}
