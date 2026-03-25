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
	/// Represents the DataRepository.BbPatientdrugdoseProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbPatientdrugdoseDataSourceDesigner))]
	public class BbPatientdrugdoseDataSource : ProviderDataSource<BbPatientdrugdose, BbPatientdrugdoseKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientdrugdoseDataSource class.
		/// </summary>
		public BbPatientdrugdoseDataSource() : base(new BbPatientdrugdoseService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbPatientdrugdoseDataSourceView used by the BbPatientdrugdoseDataSource.
		/// </summary>
		protected BbPatientdrugdoseDataSourceView BbPatientdrugdoseView
		{
			get { return ( View as BbPatientdrugdoseDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbPatientdrugdoseDataSource control invokes to retrieve data.
		/// </summary>
		public BbPatientdrugdoseSelectMethod SelectMethod
		{
			get
			{
				BbPatientdrugdoseSelectMethod selectMethod = BbPatientdrugdoseSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbPatientdrugdoseSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbPatientdrugdoseDataSourceView class that is to be
		/// used by the BbPatientdrugdoseDataSource.
		/// </summary>
		/// <returns>An instance of the BbPatientdrugdoseDataSourceView class.</returns>
		protected override BaseDataSourceView<BbPatientdrugdose, BbPatientdrugdoseKey> GetNewDataSourceView()
		{
			return new BbPatientdrugdoseDataSourceView(this, DefaultViewName);
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
	/// Supports the BbPatientdrugdoseDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbPatientdrugdoseDataSourceView : ProviderDataSourceView<BbPatientdrugdose, BbPatientdrugdoseKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientdrugdoseDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbPatientdrugdoseDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbPatientdrugdoseDataSourceView(BbPatientdrugdoseDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbPatientdrugdoseDataSource BbPatientdrugdoseOwner
		{
			get { return Owner as BbPatientdrugdoseDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbPatientdrugdoseSelectMethod SelectMethod
		{
			get { return BbPatientdrugdoseOwner.SelectMethod; }
			set { BbPatientdrugdoseOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbPatientdrugdoseService BbPatientdrugdoseProvider
		{
			get { return Provider as BbPatientdrugdoseService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbPatientdrugdose> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbPatientdrugdose> results = null;
			BbPatientdrugdose item;
			count = 0;
			
			int _patdrugid;
			int _doseid;
			System.Int32? sp201_Patdrugid;

			switch ( SelectMethod )
			{
				case BbPatientdrugdoseSelectMethod.Get:
					BbPatientdrugdoseKey entityKey  = new BbPatientdrugdoseKey();
					entityKey.Load(values);
					item = BbPatientdrugdoseProvider.Get(entityKey);
					results = new TList<BbPatientdrugdose>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbPatientdrugdoseSelectMethod.GetAll:
                    results = BbPatientdrugdoseProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbPatientdrugdoseSelectMethod.GetPaged:
					results = BbPatientdrugdoseProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbPatientdrugdoseSelectMethod.Find:
					if ( FilterParameters != null )
						results = BbPatientdrugdoseProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbPatientdrugdoseProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbPatientdrugdoseSelectMethod.GetByDoseid:
					_doseid = ( values["Doseid"] != null ) ? (int) EntityUtil.ChangeType(values["Doseid"], typeof(int)) : (int)0;
					item = BbPatientdrugdoseProvider.GetByDoseid(_doseid);
					results = new TList<BbPatientdrugdose>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				case BbPatientdrugdoseSelectMethod.GetByPatdrugid:
					_patdrugid = ( values["Patdrugid"] != null ) ? (int) EntityUtil.ChangeType(values["Patdrugid"], typeof(int)) : (int)0;
					results = BbPatientdrugdoseProvider.GetByPatdrugid(_patdrugid, this.StartIndex, this.PageSize, out count);
					break;
				// FK
				// M:M
				// Custom
				case BbPatientdrugdoseSelectMethod.PatientDrug_GetPreviousDoses:
					sp201_Patdrugid = (System.Int32?) EntityUtil.ChangeType(values["Patdrugid"], typeof(System.Int32?));
					results = BbPatientdrugdoseProvider.PatientDrug_GetPreviousDoses(sp201_Patdrugid, StartIndex, PageSize);
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
			if ( SelectMethod == BbPatientdrugdoseSelectMethod.Get || SelectMethod == BbPatientdrugdoseSelectMethod.GetByDoseid )
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
				BbPatientdrugdose entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbPatientdrugdoseProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<BbPatientdrugdose> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbPatientdrugdoseProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbPatientdrugdoseDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbPatientdrugdoseDataSource class.
	/// </summary>
	public class BbPatientdrugdoseDataSourceDesigner : ProviderDataSourceDesigner<BbPatientdrugdose, BbPatientdrugdoseKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbPatientdrugdoseDataSourceDesigner class.
		/// </summary>
		public BbPatientdrugdoseDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbPatientdrugdoseSelectMethod SelectMethod
		{
			get { return ((BbPatientdrugdoseDataSource) DataSource).SelectMethod; }
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
				actions.Add(new BbPatientdrugdoseDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbPatientdrugdoseDataSourceActionList

	/// <summary>
	/// Supports the BbPatientdrugdoseDataSourceDesigner class.
	/// </summary>
	internal class BbPatientdrugdoseDataSourceActionList : DesignerActionList
	{
		private BbPatientdrugdoseDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbPatientdrugdoseDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbPatientdrugdoseDataSourceActionList(BbPatientdrugdoseDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbPatientdrugdoseSelectMethod SelectMethod
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

	#endregion BbPatientdrugdoseDataSourceActionList
	
	#endregion BbPatientdrugdoseDataSourceDesigner
	
	#region BbPatientdrugdoseSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbPatientdrugdoseDataSource.SelectMethod property.
	/// </summary>
	public enum BbPatientdrugdoseSelectMethod
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
		/// Represents the GetByPatdrugid method.
		/// </summary>
		GetByPatdrugid,
		/// <summary>
		/// Represents the GetByDoseid method.
		/// </summary>
		GetByDoseid,
		/// <summary>
		/// Represents the PatientDrug_GetPreviousDoses method.
		/// </summary>
		PatientDrug_GetPreviousDoses
	}
	
	#endregion BbPatientdrugdoseSelectMethod

	#region BbPatientdrugdoseFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatientdrugdose"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientdrugdoseFilter : SqlFilter<BbPatientdrugdoseColumn>
	{
	}
	
	#endregion BbPatientdrugdoseFilter

	#region BbPatientdrugdoseExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatientdrugdose"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientdrugdoseExpressionBuilder : SqlExpressionBuilder<BbPatientdrugdoseColumn>
	{
	}
	
	#endregion BbPatientdrugdoseExpressionBuilder	

	#region BbPatientdrugdoseProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbPatientdrugdoseChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatientdrugdose"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientdrugdoseProperty : ChildEntityProperty<BbPatientdrugdoseChildEntityTypes>
	{
	}
	
	#endregion BbPatientdrugdoseProperty
}

