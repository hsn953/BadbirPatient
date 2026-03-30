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
	/// Represents the DataRepository.BbPatientProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbPatientDataSourceDesigner))]
	public class BbPatientDataSource : ProviderDataSource<BbPatient, BbPatientKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientDataSource class.
		/// </summary>
		public BbPatientDataSource() : base(new BbPatientService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbPatientDataSourceView used by the BbPatientDataSource.
		/// </summary>
		protected BbPatientDataSourceView BbPatientView
		{
			get { return ( View as BbPatientDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbPatientDataSource control invokes to retrieve data.
		/// </summary>
		public BbPatientSelectMethod SelectMethod
		{
			get
			{
				BbPatientSelectMethod selectMethod = BbPatientSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbPatientSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbPatientDataSourceView class that is to be
		/// used by the BbPatientDataSource.
		/// </summary>
		/// <returns>An instance of the BbPatientDataSourceView class.</returns>
		protected override BaseDataSourceView<BbPatient, BbPatientKey> GetNewDataSourceView()
		{
			return new BbPatientDataSourceView(this, DefaultViewName);
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
	/// Supports the BbPatientDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbPatientDataSourceView : ProviderDataSourceView<BbPatient, BbPatientKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbPatientDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbPatientDataSourceView(BbPatientDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbPatientDataSource BbPatientOwner
		{
			get { return Owner as BbPatientDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbPatientSelectMethod SelectMethod
		{
			get { return BbPatientOwner.SelectMethod; }
			set { BbPatientOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbPatientService BbPatientProvider
		{
			get { return Provider as BbPatientService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbPatient> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbPatient> results = null;
			BbPatient item;
			count = 0;
			
			int _patientid;
			int _statusdetailid;
			System.Int32? _statusid_nullable;
			System.Int32? _genderid_nullable;
			System.Int32 _externalStudyId;
			System.Int32? sp120_OldBadbirUserId;
			System.Int32? sp120_NewBadbirUserId;
			System.Int32? sp162_Centreid;
			System.Int32? sp163_Centreid;

			switch ( SelectMethod )
			{
				case BbPatientSelectMethod.Get:
					BbPatientKey entityKey  = new BbPatientKey();
					entityKey.Load(values);
					item = BbPatientProvider.Get(entityKey);
					results = new TList<BbPatient>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbPatientSelectMethod.GetAll:
                    results = BbPatientProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbPatientSelectMethod.GetPaged:
					results = BbPatientProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbPatientSelectMethod.Find:
					if ( FilterParameters != null )
						results = BbPatientProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbPatientProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbPatientSelectMethod.GetByPatientid:
					_patientid = ( values["Patientid"] != null ) ? (int) EntityUtil.ChangeType(values["Patientid"], typeof(int)) : (int)0;
					item = BbPatientProvider.GetByPatientid(_patientid);
					results = new TList<BbPatient>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case BbPatientSelectMethod.GetByStatusdetailid:
					_statusdetailid = ( values["Statusdetailid"] != null ) ? (int) EntityUtil.ChangeType(values["Statusdetailid"], typeof(int)) : (int)0;
					results = BbPatientProvider.GetByStatusdetailid(_statusdetailid, this.StartIndex, this.PageSize, out count);
					break;
				case BbPatientSelectMethod.GetByStatusid:
					_statusid_nullable = (System.Int32?) EntityUtil.ChangeType(values["Statusid"], typeof(System.Int32?));
					results = BbPatientProvider.GetByStatusid(_statusid_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case BbPatientSelectMethod.GetByGenderid:
					_genderid_nullable = (System.Int32?) EntityUtil.ChangeType(values["Genderid"], typeof(System.Int32?));
					results = BbPatientProvider.GetByGenderid(_genderid_nullable, this.StartIndex, this.PageSize, out count);
					break;
				// M:M
				case BbPatientSelectMethod.GetByExternalStudyIdFromBbPatientExternalStudyLink:
					_externalStudyId = ( values["ExternalStudyId"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["ExternalStudyId"], typeof(System.Int32)) : (int)0;
					results = BbPatientProvider.GetByExternalStudyIdFromBbPatientExternalStudyLink(_externalStudyId, this.StartIndex, this.PageSize, out count);
					break;
				// Custom
				case BbPatientSelectMethod.DelegationLog_mergeAnonymousAccountWithNewAccount:
					sp120_OldBadbirUserId = (System.Int32?) EntityUtil.ChangeType(values["OldBadbirUserId"], typeof(System.Int32?));
					sp120_NewBadbirUserId = (System.Int32?) EntityUtil.ChangeType(values["NewBadbirUserId"], typeof(System.Int32?));
					results = BbPatientProvider.DelegationLog_mergeAnonymousAccountWithNewAccount(sp120_OldBadbirUserId, sp120_NewBadbirUserId, StartIndex, PageSize);
					break;
				case BbPatientSelectMethod.Patient_Get_List_byCentre:
					sp162_Centreid = (System.Int32?) EntityUtil.ChangeType(values["Centreid"], typeof(System.Int32?));
					results = BbPatientProvider.Patient_Get_List_byCentre(sp162_Centreid, StartIndex, PageSize);
					break;
				case BbPatientSelectMethod.Patient_Get_List_byCentre_InEditWindow:
					sp163_Centreid = (System.Int32?) EntityUtil.ChangeType(values["Centreid"], typeof(System.Int32?));
					results = BbPatientProvider.Patient_Get_List_byCentre_InEditWindow(sp163_Centreid, StartIndex, PageSize);
					break;
				case BbPatientSelectMethod.Patient_Get_NHSICNotSent:
					results = BbPatientProvider.Patient_Get_NHSICNotSent(StartIndex, PageSize);
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
			if ( SelectMethod == BbPatientSelectMethod.Get || SelectMethod == BbPatientSelectMethod.GetByPatientid )
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
				BbPatient entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbPatientProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<BbPatient> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbPatientProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbPatientDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbPatientDataSource class.
	/// </summary>
	public class BbPatientDataSourceDesigner : ProviderDataSourceDesigner<BbPatient, BbPatientKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbPatientDataSourceDesigner class.
		/// </summary>
		public BbPatientDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbPatientSelectMethod SelectMethod
		{
			get { return ((BbPatientDataSource) DataSource).SelectMethod; }
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
				actions.Add(new BbPatientDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbPatientDataSourceActionList

	/// <summary>
	/// Supports the BbPatientDataSourceDesigner class.
	/// </summary>
	internal class BbPatientDataSourceActionList : DesignerActionList
	{
		private BbPatientDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbPatientDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbPatientDataSourceActionList(BbPatientDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbPatientSelectMethod SelectMethod
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

	#endregion BbPatientDataSourceActionList
	
	#endregion BbPatientDataSourceDesigner
	
	#region BbPatientSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbPatientDataSource.SelectMethod property.
	/// </summary>
	public enum BbPatientSelectMethod
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
		/// Represents the GetByPatientid method.
		/// </summary>
		GetByPatientid,
		/// <summary>
		/// Represents the GetByStatusdetailid method.
		/// </summary>
		GetByStatusdetailid,
		/// <summary>
		/// Represents the GetByStatusid method.
		/// </summary>
		GetByStatusid,
		/// <summary>
		/// Represents the GetByGenderid method.
		/// </summary>
		GetByGenderid,
		/// <summary>
		/// Represents the GetByExternalStudyIdFromBbPatientExternalStudyLink method.
		/// </summary>
		GetByExternalStudyIdFromBbPatientExternalStudyLink,
		/// <summary>
		/// Represents the DelegationLog_mergeAnonymousAccountWithNewAccount method.
		/// </summary>
		DelegationLog_mergeAnonymousAccountWithNewAccount,
		/// <summary>
		/// Represents the Patient_Get_List_byCentre method.
		/// </summary>
		Patient_Get_List_byCentre,
		/// <summary>
		/// Represents the Patient_Get_List_byCentre_InEditWindow method.
		/// </summary>
		Patient_Get_List_byCentre_InEditWindow,
		/// <summary>
		/// Represents the Patient_Get_NHSICNotSent method.
		/// </summary>
		Patient_Get_NHSICNotSent
	}
	
	#endregion BbPatientSelectMethod

	#region BbPatientFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatient"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientFilter : SqlFilter<BbPatientColumn>
	{
	}
	
	#endregion BbPatientFilter

	#region BbPatientExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatient"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientExpressionBuilder : SqlExpressionBuilder<BbPatientColumn>
	{
	}
	
	#endregion BbPatientExpressionBuilder	

	#region BbPatientProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbPatientChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatient"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientProperty : ChildEntityProperty<BbPatientChildEntityTypes>
	{
	}
	
	#endregion BbPatientProperty
}

