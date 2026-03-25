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
	/// Represents the DataRepository.BbFileStorageProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbFileStorageDataSourceDesigner))]
	public class BbFileStorageDataSource : ProviderDataSource<BbFileStorage, BbFileStorageKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbFileStorageDataSource class.
		/// </summary>
		public BbFileStorageDataSource() : base(new BbFileStorageService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbFileStorageDataSourceView used by the BbFileStorageDataSource.
		/// </summary>
		protected BbFileStorageDataSourceView BbFileStorageView
		{
			get { return ( View as BbFileStorageDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbFileStorageDataSource control invokes to retrieve data.
		/// </summary>
		public BbFileStorageSelectMethod SelectMethod
		{
			get
			{
				BbFileStorageSelectMethod selectMethod = BbFileStorageSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbFileStorageSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbFileStorageDataSourceView class that is to be
		/// used by the BbFileStorageDataSource.
		/// </summary>
		/// <returns>An instance of the BbFileStorageDataSourceView class.</returns>
		protected override BaseDataSourceView<BbFileStorage, BbFileStorageKey> GetNewDataSourceView()
		{
			return new BbFileStorageDataSourceView(this, DefaultViewName);
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
	/// Supports the BbFileStorageDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbFileStorageDataSourceView : ProviderDataSourceView<BbFileStorage, BbFileStorageKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbFileStorageDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbFileStorageDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbFileStorageDataSourceView(BbFileStorageDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbFileStorageDataSource BbFileStorageOwner
		{
			get { return Owner as BbFileStorageDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbFileStorageSelectMethod SelectMethod
		{
			get { return BbFileStorageOwner.SelectMethod; }
			set { BbFileStorageOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbFileStorageService BbFileStorageProvider
		{
			get { return Provider as BbFileStorageService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbFileStorage> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbFileStorage> results = null;
			BbFileStorage item;
			count = 0;
			
			int _foreignKey;
			int _foreignKeyType;
			int _fileId;
			System.Int32? sp147_PatientId;

			switch ( SelectMethod )
			{
				case BbFileStorageSelectMethod.Get:
					BbFileStorageKey entityKey  = new BbFileStorageKey();
					entityKey.Load(values);
					item = BbFileStorageProvider.Get(entityKey);
					results = new TList<BbFileStorage>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbFileStorageSelectMethod.GetAll:
                    results = BbFileStorageProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbFileStorageSelectMethod.GetPaged:
					results = BbFileStorageProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbFileStorageSelectMethod.Find:
					if ( FilterParameters != null )
						results = BbFileStorageProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbFileStorageProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbFileStorageSelectMethod.GetByFileId:
					_fileId = ( values["FileId"] != null ) ? (int) EntityUtil.ChangeType(values["FileId"], typeof(int)) : (int)0;
					item = BbFileStorageProvider.GetByFileId(_fileId);
					results = new TList<BbFileStorage>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				case BbFileStorageSelectMethod.GetByForeignKeyForeignKeyType:
					_foreignKey = ( values["ForeignKey"] != null ) ? (int) EntityUtil.ChangeType(values["ForeignKey"], typeof(int)) : (int)0;
					_foreignKeyType = ( values["ForeignKeyType"] != null ) ? (int) EntityUtil.ChangeType(values["ForeignKeyType"], typeof(int)) : (int)0;
					results = BbFileStorageProvider.GetByForeignKeyForeignKeyType(_foreignKey, _foreignKeyType, this.StartIndex, this.PageSize, out count);
					break;
				// FK
				// M:M
				// Custom
				case BbFileStorageSelectMethod.FileStorage_GetByPatientID:
					sp147_PatientId = (System.Int32?) EntityUtil.ChangeType(values["PatientId"], typeof(System.Int32?));
					results = BbFileStorageProvider.FileStorage_GetByPatientID(sp147_PatientId, StartIndex, PageSize);
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
			if ( SelectMethod == BbFileStorageSelectMethod.Get || SelectMethod == BbFileStorageSelectMethod.GetByFileId )
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
				BbFileStorage entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbFileStorageProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<BbFileStorage> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbFileStorageProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbFileStorageDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbFileStorageDataSource class.
	/// </summary>
	public class BbFileStorageDataSourceDesigner : ProviderDataSourceDesigner<BbFileStorage, BbFileStorageKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbFileStorageDataSourceDesigner class.
		/// </summary>
		public BbFileStorageDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbFileStorageSelectMethod SelectMethod
		{
			get { return ((BbFileStorageDataSource) DataSource).SelectMethod; }
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
				actions.Add(new BbFileStorageDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbFileStorageDataSourceActionList

	/// <summary>
	/// Supports the BbFileStorageDataSourceDesigner class.
	/// </summary>
	internal class BbFileStorageDataSourceActionList : DesignerActionList
	{
		private BbFileStorageDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbFileStorageDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbFileStorageDataSourceActionList(BbFileStorageDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbFileStorageSelectMethod SelectMethod
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

	#endregion BbFileStorageDataSourceActionList
	
	#endregion BbFileStorageDataSourceDesigner
	
	#region BbFileStorageSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbFileStorageDataSource.SelectMethod property.
	/// </summary>
	public enum BbFileStorageSelectMethod
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
		/// Represents the GetByForeignKeyForeignKeyType method.
		/// </summary>
		GetByForeignKeyForeignKeyType,
		/// <summary>
		/// Represents the GetByFileId method.
		/// </summary>
		GetByFileId,
		/// <summary>
		/// Represents the FileStorage_GetByPatientID method.
		/// </summary>
		FileStorage_GetByPatientID
	}
	
	#endregion BbFileStorageSelectMethod

	#region BbFileStorageFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbFileStorage"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbFileStorageFilter : SqlFilter<BbFileStorageColumn>
	{
	}
	
	#endregion BbFileStorageFilter

	#region BbFileStorageExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbFileStorage"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbFileStorageExpressionBuilder : SqlExpressionBuilder<BbFileStorageColumn>
	{
	}
	
	#endregion BbFileStorageExpressionBuilder	

	#region BbFileStorageProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbFileStorageChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbFileStorage"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbFileStorageProperty : ChildEntityProperty<BbFileStorageChildEntityTypes>
	{
	}
	
	#endregion BbFileStorageProperty
}

