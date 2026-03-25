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
	/// Represents the DataRepository.BbAdditionalUserAndCentreProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbAdditionalUserAndCentreDataSourceDesigner))]
	public class BbAdditionalUserAndCentreDataSource : ProviderDataSource<BbAdditionalUserAndCentre, BbAdditionalUserAndCentreKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbAdditionalUserAndCentreDataSource class.
		/// </summary>
		public BbAdditionalUserAndCentreDataSource() : base(new BbAdditionalUserAndCentreService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbAdditionalUserAndCentreDataSourceView used by the BbAdditionalUserAndCentreDataSource.
		/// </summary>
		protected BbAdditionalUserAndCentreDataSourceView BbAdditionalUserAndCentreView
		{
			get { return ( View as BbAdditionalUserAndCentreDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbAdditionalUserAndCentreDataSource control invokes to retrieve data.
		/// </summary>
		public BbAdditionalUserAndCentreSelectMethod SelectMethod
		{
			get
			{
				BbAdditionalUserAndCentreSelectMethod selectMethod = BbAdditionalUserAndCentreSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbAdditionalUserAndCentreSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbAdditionalUserAndCentreDataSourceView class that is to be
		/// used by the BbAdditionalUserAndCentreDataSource.
		/// </summary>
		/// <returns>An instance of the BbAdditionalUserAndCentreDataSourceView class.</returns>
		protected override BaseDataSourceView<BbAdditionalUserAndCentre, BbAdditionalUserAndCentreKey> GetNewDataSourceView()
		{
			return new BbAdditionalUserAndCentreDataSourceView(this, DefaultViewName);
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
	/// Supports the BbAdditionalUserAndCentreDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbAdditionalUserAndCentreDataSourceView : ProviderDataSourceView<BbAdditionalUserAndCentre, BbAdditionalUserAndCentreKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbAdditionalUserAndCentreDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbAdditionalUserAndCentreDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbAdditionalUserAndCentreDataSourceView(BbAdditionalUserAndCentreDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbAdditionalUserAndCentreDataSource BbAdditionalUserAndCentreOwner
		{
			get { return Owner as BbAdditionalUserAndCentreDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbAdditionalUserAndCentreSelectMethod SelectMethod
		{
			get { return BbAdditionalUserAndCentreOwner.SelectMethod; }
			set { BbAdditionalUserAndCentreOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbAdditionalUserAndCentreService BbAdditionalUserAndCentreProvider
		{
			get { return Provider as BbAdditionalUserAndCentreService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbAdditionalUserAndCentre> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbAdditionalUserAndCentre> results = null;
			BbAdditionalUserAndCentre item;
			count = 0;
			
			System.Guid _userid;
			int _centreid;

			switch ( SelectMethod )
			{
				case BbAdditionalUserAndCentreSelectMethod.Get:
					BbAdditionalUserAndCentreKey entityKey  = new BbAdditionalUserAndCentreKey();
					entityKey.Load(values);
					item = BbAdditionalUserAndCentreProvider.Get(entityKey);
					results = new TList<BbAdditionalUserAndCentre>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbAdditionalUserAndCentreSelectMethod.GetAll:
                    results = BbAdditionalUserAndCentreProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbAdditionalUserAndCentreSelectMethod.GetPaged:
					results = BbAdditionalUserAndCentreProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbAdditionalUserAndCentreSelectMethod.Find:
					if ( FilterParameters != null )
						results = BbAdditionalUserAndCentreProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbAdditionalUserAndCentreProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbAdditionalUserAndCentreSelectMethod.GetByUseridCentreid:
					_userid = ( values["Userid"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["Userid"], typeof(System.Guid)) : Guid.Empty;
					_centreid = ( values["Centreid"] != null ) ? (int) EntityUtil.ChangeType(values["Centreid"], typeof(int)) : (int)0;
					item = BbAdditionalUserAndCentreProvider.GetByUseridCentreid(_userid, _centreid);
					results = new TList<BbAdditionalUserAndCentre>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case BbAdditionalUserAndCentreSelectMethod.GetByUserid:
					_userid = ( values["Userid"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["Userid"], typeof(System.Guid)) : Guid.Empty;
					results = BbAdditionalUserAndCentreProvider.GetByUserid(_userid, this.StartIndex, this.PageSize, out count);
					break;
				case BbAdditionalUserAndCentreSelectMethod.GetByCentreid:
					_centreid = ( values["Centreid"] != null ) ? (int) EntityUtil.ChangeType(values["Centreid"], typeof(int)) : (int)0;
					results = BbAdditionalUserAndCentreProvider.GetByCentreid(_centreid, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == BbAdditionalUserAndCentreSelectMethod.Get || SelectMethod == BbAdditionalUserAndCentreSelectMethod.GetByUseridCentreid )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Sets the primary key values of the specified Entity object.
		/// </summary>
		/// <param name="entity">The Entity object to update.</param>
		protected override void SetEntityKeyValues(BbAdditionalUserAndCentre entity)
		{
			base.SetEntityKeyValues(entity);
			
			// make sure primary key column(s) have been set
			if ( entity.Userid == Guid.Empty )
				entity.Userid = Guid.NewGuid();
		}
		
		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				BbAdditionalUserAndCentre entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbAdditionalUserAndCentreProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<BbAdditionalUserAndCentre> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbAdditionalUserAndCentreProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbAdditionalUserAndCentreDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbAdditionalUserAndCentreDataSource class.
	/// </summary>
	public class BbAdditionalUserAndCentreDataSourceDesigner : ProviderDataSourceDesigner<BbAdditionalUserAndCentre, BbAdditionalUserAndCentreKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbAdditionalUserAndCentreDataSourceDesigner class.
		/// </summary>
		public BbAdditionalUserAndCentreDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbAdditionalUserAndCentreSelectMethod SelectMethod
		{
			get { return ((BbAdditionalUserAndCentreDataSource) DataSource).SelectMethod; }
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
				actions.Add(new BbAdditionalUserAndCentreDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbAdditionalUserAndCentreDataSourceActionList

	/// <summary>
	/// Supports the BbAdditionalUserAndCentreDataSourceDesigner class.
	/// </summary>
	internal class BbAdditionalUserAndCentreDataSourceActionList : DesignerActionList
	{
		private BbAdditionalUserAndCentreDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbAdditionalUserAndCentreDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbAdditionalUserAndCentreDataSourceActionList(BbAdditionalUserAndCentreDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbAdditionalUserAndCentreSelectMethod SelectMethod
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

	#endregion BbAdditionalUserAndCentreDataSourceActionList
	
	#endregion BbAdditionalUserAndCentreDataSourceDesigner
	
	#region BbAdditionalUserAndCentreSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbAdditionalUserAndCentreDataSource.SelectMethod property.
	/// </summary>
	public enum BbAdditionalUserAndCentreSelectMethod
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
		/// Represents the GetByUseridCentreid method.
		/// </summary>
		GetByUseridCentreid,
		/// <summary>
		/// Represents the GetByUserid method.
		/// </summary>
		GetByUserid,
		/// <summary>
		/// Represents the GetByCentreid method.
		/// </summary>
		GetByCentreid
	}
	
	#endregion BbAdditionalUserAndCentreSelectMethod

	#region BbAdditionalUserAndCentreFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbAdditionalUserAndCentre"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbAdditionalUserAndCentreFilter : SqlFilter<BbAdditionalUserAndCentreColumn>
	{
	}
	
	#endregion BbAdditionalUserAndCentreFilter

	#region BbAdditionalUserAndCentreExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbAdditionalUserAndCentre"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbAdditionalUserAndCentreExpressionBuilder : SqlExpressionBuilder<BbAdditionalUserAndCentreColumn>
	{
	}
	
	#endregion BbAdditionalUserAndCentreExpressionBuilder	

	#region BbAdditionalUserAndCentreProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbAdditionalUserAndCentreChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbAdditionalUserAndCentre"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbAdditionalUserAndCentreProperty : ChildEntityProperty<BbAdditionalUserAndCentreChildEntityTypes>
	{
	}
	
	#endregion BbAdditionalUserAndCentreProperty
}

