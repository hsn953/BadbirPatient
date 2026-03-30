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
	/// Represents the DataRepository.BbPappPatientCohortTrackingProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbPappPatientCohortTrackingDataSourceDesigner))]
	public class BbPappPatientCohortTrackingDataSource : ProviderDataSource<BbPappPatientCohortTracking, BbPappPatientCohortTrackingKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPappPatientCohortTrackingDataSource class.
		/// </summary>
		public BbPappPatientCohortTrackingDataSource() : base(new BbPappPatientCohortTrackingService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbPappPatientCohortTrackingDataSourceView used by the BbPappPatientCohortTrackingDataSource.
		/// </summary>
		protected BbPappPatientCohortTrackingDataSourceView BbPappPatientCohortTrackingView
		{
			get { return ( View as BbPappPatientCohortTrackingDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbPappPatientCohortTrackingDataSource control invokes to retrieve data.
		/// </summary>
		public BbPappPatientCohortTrackingSelectMethod SelectMethod
		{
			get
			{
				BbPappPatientCohortTrackingSelectMethod selectMethod = BbPappPatientCohortTrackingSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbPappPatientCohortTrackingSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbPappPatientCohortTrackingDataSourceView class that is to be
		/// used by the BbPappPatientCohortTrackingDataSource.
		/// </summary>
		/// <returns>An instance of the BbPappPatientCohortTrackingDataSourceView class.</returns>
		protected override BaseDataSourceView<BbPappPatientCohortTracking, BbPappPatientCohortTrackingKey> GetNewDataSourceView()
		{
			return new BbPappPatientCohortTrackingDataSourceView(this, DefaultViewName);
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
	/// Supports the BbPappPatientCohortTrackingDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbPappPatientCohortTrackingDataSourceView : ProviderDataSourceView<BbPappPatientCohortTracking, BbPappPatientCohortTrackingKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPappPatientCohortTrackingDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbPappPatientCohortTrackingDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbPappPatientCohortTrackingDataSourceView(BbPappPatientCohortTrackingDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbPappPatientCohortTrackingDataSource BbPappPatientCohortTrackingOwner
		{
			get { return Owner as BbPappPatientCohortTrackingDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbPappPatientCohortTrackingSelectMethod SelectMethod
		{
			get { return BbPappPatientCohortTrackingOwner.SelectMethod; }
			set { BbPappPatientCohortTrackingOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbPappPatientCohortTrackingService BbPappPatientCohortTrackingProvider
		{
			get { return Provider as BbPappPatientCohortTrackingService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbPappPatientCohortTracking> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbPappPatientCohortTracking> results = null;
			BbPappPatientCohortTracking item;
			count = 0;
			
			int _pappFupId;
			int _patientId;
			System.Int32? _importedFupId_nullable;
			System.Int32? _clinicAttendance_nullable;

			switch ( SelectMethod )
			{
				case BbPappPatientCohortTrackingSelectMethod.Get:
					BbPappPatientCohortTrackingKey entityKey  = new BbPappPatientCohortTrackingKey();
					entityKey.Load(values);
					item = BbPappPatientCohortTrackingProvider.Get(entityKey);
					results = new TList<BbPappPatientCohortTracking>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbPappPatientCohortTrackingSelectMethod.GetAll:
                    results = BbPappPatientCohortTrackingProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbPappPatientCohortTrackingSelectMethod.GetPaged:
					results = BbPappPatientCohortTrackingProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbPappPatientCohortTrackingSelectMethod.Find:
					if ( FilterParameters != null )
						results = BbPappPatientCohortTrackingProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbPappPatientCohortTrackingProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbPappPatientCohortTrackingSelectMethod.GetByPappFupId:
					_pappFupId = ( values["PappFupId"] != null ) ? (int) EntityUtil.ChangeType(values["PappFupId"], typeof(int)) : (int)0;
					item = BbPappPatientCohortTrackingProvider.GetByPappFupId(_pappFupId);
					results = new TList<BbPappPatientCohortTracking>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case BbPappPatientCohortTrackingSelectMethod.GetByPatientId:
					_patientId = ( values["PatientId"] != null ) ? (int) EntityUtil.ChangeType(values["PatientId"], typeof(int)) : (int)0;
					results = BbPappPatientCohortTrackingProvider.GetByPatientId(_patientId, this.StartIndex, this.PageSize, out count);
					break;
				case BbPappPatientCohortTrackingSelectMethod.GetByImportedFupId:
					_importedFupId_nullable = (System.Int32?) EntityUtil.ChangeType(values["ImportedFupId"], typeof(System.Int32?));
					results = BbPappPatientCohortTrackingProvider.GetByImportedFupId(_importedFupId_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case BbPappPatientCohortTrackingSelectMethod.GetByClinicAttendance:
					_clinicAttendance_nullable = (System.Int32?) EntityUtil.ChangeType(values["ClinicAttendance"], typeof(System.Int32?));
					results = BbPappPatientCohortTrackingProvider.GetByClinicAttendance(_clinicAttendance_nullable, this.StartIndex, this.PageSize, out count);
					break;
				// M:M
				// Custom
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
			if ( SelectMethod == BbPappPatientCohortTrackingSelectMethod.Get || SelectMethod == BbPappPatientCohortTrackingSelectMethod.GetByPappFupId )
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
				BbPappPatientCohortTracking entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbPappPatientCohortTrackingProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<BbPappPatientCohortTracking> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbPappPatientCohortTrackingProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbPappPatientCohortTrackingDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbPappPatientCohortTrackingDataSource class.
	/// </summary>
	public class BbPappPatientCohortTrackingDataSourceDesigner : ProviderDataSourceDesigner<BbPappPatientCohortTracking, BbPappPatientCohortTrackingKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbPappPatientCohortTrackingDataSourceDesigner class.
		/// </summary>
		public BbPappPatientCohortTrackingDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbPappPatientCohortTrackingSelectMethod SelectMethod
		{
			get { return ((BbPappPatientCohortTrackingDataSource) DataSource).SelectMethod; }
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
				actions.Add(new BbPappPatientCohortTrackingDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbPappPatientCohortTrackingDataSourceActionList

	/// <summary>
	/// Supports the BbPappPatientCohortTrackingDataSourceDesigner class.
	/// </summary>
	internal class BbPappPatientCohortTrackingDataSourceActionList : DesignerActionList
	{
		private BbPappPatientCohortTrackingDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbPappPatientCohortTrackingDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbPappPatientCohortTrackingDataSourceActionList(BbPappPatientCohortTrackingDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbPappPatientCohortTrackingSelectMethod SelectMethod
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

	#endregion BbPappPatientCohortTrackingDataSourceActionList
	
	#endregion BbPappPatientCohortTrackingDataSourceDesigner
	
	#region BbPappPatientCohortTrackingSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbPappPatientCohortTrackingDataSource.SelectMethod property.
	/// </summary>
	public enum BbPappPatientCohortTrackingSelectMethod
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
		/// Represents the GetByPappFupId method.
		/// </summary>
		GetByPappFupId,
		/// <summary>
		/// Represents the GetByPatientId method.
		/// </summary>
		GetByPatientId,
		/// <summary>
		/// Represents the GetByImportedFupId method.
		/// </summary>
		GetByImportedFupId,
		/// <summary>
		/// Represents the GetByClinicAttendance method.
		/// </summary>
		GetByClinicAttendance
	}
	
	#endregion BbPappPatientCohortTrackingSelectMethod

	#region BbPappPatientCohortTrackingFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPappPatientCohortTracking"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPappPatientCohortTrackingFilter : SqlFilter<BbPappPatientCohortTrackingColumn>
	{
	}
	
	#endregion BbPappPatientCohortTrackingFilter

	#region BbPappPatientCohortTrackingExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPappPatientCohortTracking"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPappPatientCohortTrackingExpressionBuilder : SqlExpressionBuilder<BbPappPatientCohortTrackingColumn>
	{
	}
	
	#endregion BbPappPatientCohortTrackingExpressionBuilder	

	#region BbPappPatientCohortTrackingProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbPappPatientCohortTrackingChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbPappPatientCohortTracking"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPappPatientCohortTrackingProperty : ChildEntityProperty<BbPappPatientCohortTrackingChildEntityTypes>
	{
	}
	
	#endregion BbPappPatientCohortTrackingProperty
}

