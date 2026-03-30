#region Using Directives
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Web.UI;
using System.Web.UI.Design;

using MHS.Badbir.NetTiers.Entities;
using MHS.Badbir.NetTiers.Data;
using MHS.Badbir.NetTiers.Data.Bases;
using MHS.Badbir.NetTiers.Component;
#endregion

namespace MHS.Badbir.NetTiers.Web.Data
{
	/// <summary>
	/// Represents the DataRepository.BbPatientCohortTrackingProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbPatientCohortTrackingDataSourceDesigner))]
	public class BbPatientCohortTrackingDataSource : ProviderDataSource<BbPatientCohortTracking, BbPatientCohortTrackingKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientCohortTrackingDataSource class.
		/// </summary>
		public BbPatientCohortTrackingDataSource() : base(new BbPatientCohortTrackingService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbPatientCohortTrackingDataSourceView used by the BbPatientCohortTrackingDataSource.
		/// </summary>
		protected BbPatientCohortTrackingDataSourceView BbPatientCohortTrackingView
		{
			get { return ( View as BbPatientCohortTrackingDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbPatientCohortTrackingDataSource control invokes to retrieve data.
		/// </summary>
		public BbPatientCohortTrackingSelectMethod SelectMethod
		{
			get
			{
				BbPatientCohortTrackingSelectMethod selectMethod = BbPatientCohortTrackingSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbPatientCohortTrackingSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbPatientCohortTrackingDataSourceView class that is to be
		/// used by the BbPatientCohortTrackingDataSource.
		/// </summary>
		/// <returns>An instance of the BbPatientCohortTrackingDataSourceView class.</returns>
		protected override BaseDataSourceView<BbPatientCohortTracking, BbPatientCohortTrackingKey> GetNewDataSourceView()
		{
			return new BbPatientCohortTrackingDataSourceView(this, DefaultViewName);
		}
		
		/// <summary>
        /// Creates a cache hashing key based on the startIndex, pageSize and the SelectMethod being used.
        /// </summary>
        /// <param name="startIndex">The current start row index.</param>
        /// <param name="pageSize">The current page size.</param>
        /// <returns>A string that can be used as a key for caching purposes.</returns>
		protected override string CacheHashKey(int startIndex, int pageSize)
        {
			return String.Format("{0}:{1}:{2}", SelectMethod, startIndex, pageSize);
        }
		
		#endregion Methods
	}
	
	/// <summary>
	/// Supports the BbPatientCohortTrackingDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbPatientCohortTrackingDataSourceView : ProviderDataSourceView<BbPatientCohortTracking, BbPatientCohortTrackingKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientCohortTrackingDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbPatientCohortTrackingDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbPatientCohortTrackingDataSourceView(BbPatientCohortTrackingDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbPatientCohortTrackingDataSource BbPatientCohortTrackingOwner
		{
			get { return Owner as BbPatientCohortTrackingDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbPatientCohortTrackingSelectMethod SelectMethod
		{
			get { return BbPatientCohortTrackingOwner.SelectMethod; }
			set { BbPatientCohortTrackingOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbPatientCohortTrackingService BbPatientCohortTrackingProvider
		{
			get { return Provider as BbPatientCohortTrackingService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbPatientCohortTracking> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbPatientCohortTracking> results = null;
			BbPatientCohortTracking item;
			count = 0;
			
			int _chid;
			int _fupcode;
			System.DateTime? _dateentered_nullable;
			System.DateTime? _editWindowFrom_nullable;
			System.DateTime? _editWindowTo_nullable;
			int _fupId;
			System.Int32? _centreidcurrent_nullable;
			System.Int32? _fupstatus_nullable;
			System.Int32? _clinicAttendance_nullable;
			System.Int32? sp170_Patientid;
			System.Int32? sp177_Fupid;
			System.Int32? sp179_Srcfupid;
			System.Int32? sp179_Targetfupid;

			switch ( SelectMethod )
			{
				case BbPatientCohortTrackingSelectMethod.Get:
					BbPatientCohortTrackingKey entityKey  = new BbPatientCohortTrackingKey();
					entityKey.Load(values);
					item = BbPatientCohortTrackingProvider.Get(entityKey);
					results = new TList<BbPatientCohortTracking>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbPatientCohortTrackingSelectMethod.GetAll:
                    results = BbPatientCohortTrackingProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbPatientCohortTrackingSelectMethod.GetPaged:
					results = BbPatientCohortTrackingProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbPatientCohortTrackingSelectMethod.Find:
					if ( FilterParameters != null )
						results = BbPatientCohortTrackingProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbPatientCohortTrackingProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbPatientCohortTrackingSelectMethod.GetByFupId:
					_fupId = ( values["FupId"] != null ) ? (int) EntityUtil.ChangeType(values["FupId"], typeof(int)) : (int)0;
					item = BbPatientCohortTrackingProvider.GetByFupId(_fupId);
					results = new TList<BbPatientCohortTracking>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				case BbPatientCohortTrackingSelectMethod.GetByChidFupcode:
					_chid = ( values["Chid"] != null ) ? (int) EntityUtil.ChangeType(values["Chid"], typeof(int)) : (int)0;
					_fupcode = ( values["Fupcode"] != null ) ? (int) EntityUtil.ChangeType(values["Fupcode"], typeof(int)) : (int)0;
					item = BbPatientCohortTrackingProvider.GetByChidFupcode(_chid, _fupcode);
					results = new TList<BbPatientCohortTracking>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbPatientCohortTrackingSelectMethod.GetByChid:
					_chid = ( values["Chid"] != null ) ? (int) EntityUtil.ChangeType(values["Chid"], typeof(int)) : (int)0;
					results = BbPatientCohortTrackingProvider.GetByChid(_chid, this.StartIndex, this.PageSize, out count);
					break;
				case BbPatientCohortTrackingSelectMethod.GetByDateentered:
					_dateentered_nullable = (System.DateTime?) EntityUtil.ChangeType(values["Dateentered"], typeof(System.DateTime?));
					results = BbPatientCohortTrackingProvider.GetByDateentered(_dateentered_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case BbPatientCohortTrackingSelectMethod.GetByEditWindowFromEditWindowTo:
					_editWindowFrom_nullable = (System.DateTime?) EntityUtil.ChangeType(values["EditWindowFrom"], typeof(System.DateTime?));
					_editWindowTo_nullable = (System.DateTime?) EntityUtil.ChangeType(values["EditWindowTo"], typeof(System.DateTime?));
					results = BbPatientCohortTrackingProvider.GetByEditWindowFromEditWindowTo(_editWindowFrom_nullable, _editWindowTo_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case BbPatientCohortTrackingSelectMethod.GetByEditWindowTo:
					_editWindowTo_nullable = (System.DateTime?) EntityUtil.ChangeType(values["EditWindowTo"], typeof(System.DateTime?));
					results = BbPatientCohortTrackingProvider.GetByEditWindowTo(_editWindowTo_nullable, this.StartIndex, this.PageSize, out count);
					break;
				// FK
				case BbPatientCohortTrackingSelectMethod.GetByCentreidcurrent:
					_centreidcurrent_nullable = (System.Int32?) EntityUtil.ChangeType(values["Centreidcurrent"], typeof(System.Int32?));
					results = BbPatientCohortTrackingProvider.GetByCentreidcurrent(_centreidcurrent_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case BbPatientCohortTrackingSelectMethod.GetByFupstatus:
					_fupstatus_nullable = (System.Int32?) EntityUtil.ChangeType(values["Fupstatus"], typeof(System.Int32?));
					results = BbPatientCohortTrackingProvider.GetByFupstatus(_fupstatus_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case BbPatientCohortTrackingSelectMethod.GetByClinicAttendance:
					_clinicAttendance_nullable = (System.Int32?) EntityUtil.ChangeType(values["ClinicAttendance"], typeof(System.Int32?));
					results = BbPatientCohortTrackingProvider.GetByClinicAttendance(_clinicAttendance_nullable, this.StartIndex, this.PageSize, out count);
					break;
				// M:M
				// Custom
				case BbPatientCohortTrackingSelectMethod.Patient_GetNextFupByPatientid:
					sp170_Patientid = (System.Int32?) EntityUtil.ChangeType(values["Patientid"], typeof(System.Int32?));
					results = BbPatientCohortTrackingProvider.Patient_GetNextFupByPatientid(sp170_Patientid, StartIndex, PageSize);
					break;
				case BbPatientCohortTrackingSelectMethod.PatientCohortTracking_ClearFupData:
					sp177_Fupid = (System.Int32?) EntityUtil.ChangeType(values["Fupid"], typeof(System.Int32?));
					results = BbPatientCohortTrackingProvider.PatientCohortTracking_ClearFupData(sp177_Fupid, StartIndex, PageSize);
					break;
				case BbPatientCohortTrackingSelectMethod.PatientCohortTracking_FupSwitch:
					sp179_Srcfupid = (System.Int32?) EntityUtil.ChangeType(values["Srcfupid"], typeof(System.Int32?));
					sp179_Targetfupid = (System.Int32?) EntityUtil.ChangeType(values["Targetfupid"], typeof(System.Int32?));
					results = BbPatientCohortTrackingProvider.PatientCohortTracking_FupSwitch(sp179_Srcfupid, sp179_Targetfupid, StartIndex, PageSize);
					break;
				default:
					break;
			}

			if ( results != null && count < 1 )
			{
				count = results.Count;

				if ( !String.IsNullOrEmpty(CustomMethodRecordCountParamName) )
				{
					object objCustomCount = EntityUtil.ChangeType(customOutput[CustomMethodRecordCountParamName], typeof(Int32));
					
					if ( objCustomCount != null )
					{
						count = (int) objCustomCount;
					}
				}
			}
			
			return results;
		}
		
		/// <summary>
		/// Gets the values of any supplied parameters for internal caching.
		/// </summary>
		/// <param name="values">An IDictionary object of name/value pairs.</param>
		protected override void GetSelectParameters(IDictionary values)
		{
			if ( SelectMethod == BbPatientCohortTrackingSelectMethod.Get || SelectMethod == BbPatientCohortTrackingSelectMethod.GetByFupId )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				BbPatientCohortTracking entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbPatientCohortTrackingProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
					// set loaded flag
					IsDeepLoaded = true;
				}
			}
		}

		/// <summary>
		/// Performs a DeepLoad operation on the specified entity collection.
		/// </summary>
		/// <param name="entityList"></param>
		/// <param name="properties"></param>
		internal override void DeepLoad(TList<BbPatientCohortTracking> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbPatientCohortTrackingProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbPatientCohortTrackingDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbPatientCohortTrackingDataSource class.
	/// </summary>
	public class BbPatientCohortTrackingDataSourceDesigner : ProviderDataSourceDesigner<BbPatientCohortTracking, BbPatientCohortTrackingKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbPatientCohortTrackingDataSourceDesigner class.
		/// </summary>
		public BbPatientCohortTrackingDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbPatientCohortTrackingSelectMethod SelectMethod
		{
			get { return ((BbPatientCohortTrackingDataSource) DataSource).SelectMethod; }
			set { SetPropertyValue("SelectMethod", value); }
		}

		/// <summary>Gets the designer action list collection for this designer.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.Design.DesignerActionListCollection"/>
		/// associated with this designer.</returns>
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				DesignerActionListCollection actions = new DesignerActionListCollection();
				actions.Add(new BbPatientCohortTrackingDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbPatientCohortTrackingDataSourceActionList

	/// <summary>
	/// Supports the BbPatientCohortTrackingDataSourceDesigner class.
	/// </summary>
	internal class BbPatientCohortTrackingDataSourceActionList : DesignerActionList
	{
		private BbPatientCohortTrackingDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbPatientCohortTrackingDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbPatientCohortTrackingDataSourceActionList(BbPatientCohortTrackingDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbPatientCohortTrackingSelectMethod SelectMethod
		{
			get { return _designer.SelectMethod; }
			set { _designer.SelectMethod = value; }
		}

		/// <summary>
		/// Returns the collection of <see cref="T:System.ComponentModel.Design.DesignerActionItem"/>
		/// objects contained in the list.
		/// </summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionItem"/>
		/// array that contains the items in this list.</returns>
		public override DesignerActionItemCollection GetSortedActionItems()
		{
			DesignerActionItemCollection items = new DesignerActionItemCollection();
			items.Add(new DesignerActionPropertyItem("SelectMethod", "Select Method", "Methods"));
			return items;
		}
	}

	#endregion BbPatientCohortTrackingDataSourceActionList
	
	#endregion BbPatientCohortTrackingDataSourceDesigner
	
	#region BbPatientCohortTrackingSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbPatientCohortTrackingDataSource.SelectMethod property.
	/// </summary>
	public enum BbPatientCohortTrackingSelectMethod
	{
		/// <summary>
		/// Represents the Get method.
		/// </summary>
		Get,
		/// <summary>
		/// Represents the GetAll method.
		/// </summary>
		GetAll,
		/// <summary>
		/// Represents the GetPaged method.
		/// </summary>
		GetPaged,
		/// <summary>
		/// Represents the Find method.
		/// </summary>
		Find,
		/// <summary>
		/// Represents the GetByChidFupcode method.
		/// </summary>
		GetByChidFupcode,
		/// <summary>
		/// Represents the GetByChid method.
		/// </summary>
		GetByChid,
		/// <summary>
		/// Represents the GetByDateentered method.
		/// </summary>
		GetByDateentered,
		/// <summary>
		/// Represents the GetByEditWindowFromEditWindowTo method.
		/// </summary>
		GetByEditWindowFromEditWindowTo,
		/// <summary>
		/// Represents the GetByEditWindowTo method.
		/// </summary>
		GetByEditWindowTo,
		/// <summary>
		/// Represents the GetByFupId method.
		/// </summary>
		GetByFupId,
		/// <summary>
		/// Represents the GetByCentreidcurrent method.
		/// </summary>
		GetByCentreidcurrent,
		/// <summary>
		/// Represents the GetByFupstatus method.
		/// </summary>
		GetByFupstatus,
		/// <summary>
		/// Represents the GetByClinicAttendance method.
		/// </summary>
		GetByClinicAttendance,
		/// <summary>
		/// Represents the Patient_GetNextFupByPatientid method.
		/// </summary>
		Patient_GetNextFupByPatientid,
		/// <summary>
		/// Represents the PatientCohortTracking_ClearFupData method.
		/// </summary>
		PatientCohortTracking_ClearFupData,
		/// <summary>
		/// Represents the PatientCohortTracking_FupSwitch method.
		/// </summary>
		PatientCohortTracking_FupSwitch
	}
	
	#endregion BbPatientCohortTrackingSelectMethod

	#region BbPatientCohortTrackingFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatientCohortTracking"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientCohortTrackingFilter : SqlFilter<BbPatientCohortTrackingColumn>
	{
	}
	
	#endregion BbPatientCohortTrackingFilter

	#region BbPatientCohortTrackingExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatientCohortTracking"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientCohortTrackingExpressionBuilder : SqlExpressionBuilder<BbPatientCohortTrackingColumn>
	{
	}
	
	#endregion BbPatientCohortTrackingExpressionBuilder	

	#region BbPatientCohortTrackingProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbPatientCohortTrackingChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatientCohortTracking"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientCohortTrackingProperty : ChildEntityProperty<BbPatientCohortTrackingChildEntityTypes>
	{
	}
	
	#endregion BbPatientCohortTrackingProperty
}

