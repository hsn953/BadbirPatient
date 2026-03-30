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
	/// Represents the DataRepository.BbPappPatientMedProblemFupProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbPappPatientMedProblemFupDataSourceDesigner))]
	public class BbPappPatientMedProblemFupDataSource : ProviderDataSource<BbPappPatientMedProblemFup, BbPappPatientMedProblemFupKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPappPatientMedProblemFupDataSource class.
		/// </summary>
		public BbPappPatientMedProblemFupDataSource() : base(new BbPappPatientMedProblemFupService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbPappPatientMedProblemFupDataSourceView used by the BbPappPatientMedProblemFupDataSource.
		/// </summary>
		protected BbPappPatientMedProblemFupDataSourceView BbPappPatientMedProblemFupView
		{
			get { return ( View as BbPappPatientMedProblemFupDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbPappPatientMedProblemFupDataSource control invokes to retrieve data.
		/// </summary>
		public BbPappPatientMedProblemFupSelectMethod SelectMethod
		{
			get
			{
				BbPappPatientMedProblemFupSelectMethod selectMethod = BbPappPatientMedProblemFupSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbPappPatientMedProblemFupSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbPappPatientMedProblemFupDataSourceView class that is to be
		/// used by the BbPappPatientMedProblemFupDataSource.
		/// </summary>
		/// <returns>An instance of the BbPappPatientMedProblemFupDataSourceView class.</returns>
		protected override BaseDataSourceView<BbPappPatientMedProblemFup, BbPappPatientMedProblemFupKey> GetNewDataSourceView()
		{
			return new BbPappPatientMedProblemFupDataSourceView(this, DefaultViewName);
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
	/// Supports the BbPappPatientMedProblemFupDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbPappPatientMedProblemFupDataSourceView : ProviderDataSourceView<BbPappPatientMedProblemFup, BbPappPatientMedProblemFupKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPappPatientMedProblemFupDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbPappPatientMedProblemFupDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbPappPatientMedProblemFupDataSourceView(BbPappPatientMedProblemFupDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbPappPatientMedProblemFupDataSource BbPappPatientMedProblemFupOwner
		{
			get { return Owner as BbPappPatientMedProblemFupDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbPappPatientMedProblemFupSelectMethod SelectMethod
		{
			get { return BbPappPatientMedProblemFupOwner.SelectMethod; }
			set { BbPappPatientMedProblemFupOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbPappPatientMedProblemFupService BbPappPatientMedProblemFupProvider
		{
			get { return Provider as BbPappPatientMedProblemFupService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbPappPatientMedProblemFup> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbPappPatientMedProblemFup> results = null;
			BbPappPatientMedProblemFup item;
			count = 0;
			
			int _formId;
			int _chid;

			switch ( SelectMethod )
			{
				case BbPappPatientMedProblemFupSelectMethod.Get:
					BbPappPatientMedProblemFupKey entityKey  = new BbPappPatientMedProblemFupKey();
					entityKey.Load(values);
					item = BbPappPatientMedProblemFupProvider.Get(entityKey);
					results = new TList<BbPappPatientMedProblemFup>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbPappPatientMedProblemFupSelectMethod.GetAll:
                    results = BbPappPatientMedProblemFupProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbPappPatientMedProblemFupSelectMethod.GetPaged:
					results = BbPappPatientMedProblemFupProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbPappPatientMedProblemFupSelectMethod.Find:
					if ( FilterParameters != null )
						results = BbPappPatientMedProblemFupProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbPappPatientMedProblemFupProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbPappPatientMedProblemFupSelectMethod.GetByFormId:
					_formId = ( values["FormId"] != null ) ? (int) EntityUtil.ChangeType(values["FormId"], typeof(int)) : (int)0;
					item = BbPappPatientMedProblemFupProvider.GetByFormId(_formId);
					results = new TList<BbPappPatientMedProblemFup>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case BbPappPatientMedProblemFupSelectMethod.GetByChid:
					_chid = ( values["Chid"] != null ) ? (int) EntityUtil.ChangeType(values["Chid"], typeof(int)) : (int)0;
					results = BbPappPatientMedProblemFupProvider.GetByChid(_chid, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == BbPappPatientMedProblemFupSelectMethod.Get || SelectMethod == BbPappPatientMedProblemFupSelectMethod.GetByFormId )
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
				BbPappPatientMedProblemFup entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbPappPatientMedProblemFupProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<BbPappPatientMedProblemFup> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbPappPatientMedProblemFupProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbPappPatientMedProblemFupDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbPappPatientMedProblemFupDataSource class.
	/// </summary>
	public class BbPappPatientMedProblemFupDataSourceDesigner : ProviderDataSourceDesigner<BbPappPatientMedProblemFup, BbPappPatientMedProblemFupKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbPappPatientMedProblemFupDataSourceDesigner class.
		/// </summary>
		public BbPappPatientMedProblemFupDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbPappPatientMedProblemFupSelectMethod SelectMethod
		{
			get { return ((BbPappPatientMedProblemFupDataSource) DataSource).SelectMethod; }
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
				actions.Add(new BbPappPatientMedProblemFupDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbPappPatientMedProblemFupDataSourceActionList

	/// <summary>
	/// Supports the BbPappPatientMedProblemFupDataSourceDesigner class.
	/// </summary>
	internal class BbPappPatientMedProblemFupDataSourceActionList : DesignerActionList
	{
		private BbPappPatientMedProblemFupDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbPappPatientMedProblemFupDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbPappPatientMedProblemFupDataSourceActionList(BbPappPatientMedProblemFupDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbPappPatientMedProblemFupSelectMethod SelectMethod
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

	#endregion BbPappPatientMedProblemFupDataSourceActionList
	
	#endregion BbPappPatientMedProblemFupDataSourceDesigner
	
	#region BbPappPatientMedProblemFupSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbPappPatientMedProblemFupDataSource.SelectMethod property.
	/// </summary>
	public enum BbPappPatientMedProblemFupSelectMethod
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
		/// Represents the GetByFormId method.
		/// </summary>
		GetByFormId,
		/// <summary>
		/// Represents the GetByChid method.
		/// </summary>
		GetByChid
	}
	
	#endregion BbPappPatientMedProblemFupSelectMethod

	#region BbPappPatientMedProblemFupFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPappPatientMedProblemFup"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPappPatientMedProblemFupFilter : SqlFilter<BbPappPatientMedProblemFupColumn>
	{
	}
	
	#endregion BbPappPatientMedProblemFupFilter

	#region BbPappPatientMedProblemFupExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPappPatientMedProblemFup"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPappPatientMedProblemFupExpressionBuilder : SqlExpressionBuilder<BbPappPatientMedProblemFupColumn>
	{
	}
	
	#endregion BbPappPatientMedProblemFupExpressionBuilder	

	#region BbPappPatientMedProblemFupProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbPappPatientMedProblemFupChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbPappPatientMedProblemFup"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPappPatientMedProblemFupProperty : ChildEntityProperty<BbPappPatientMedProblemFupChildEntityTypes>
	{
	}
	
	#endregion BbPappPatientMedProblemFupProperty
}

