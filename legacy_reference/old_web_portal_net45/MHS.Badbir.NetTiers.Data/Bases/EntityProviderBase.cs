#region Using Directives
using System;
using System.Reflection;
using System.Web;
using System.Web.Configuration;
using MHS.Badbir.NetTiers.Entities;
#endregion

namespace MHS.Badbir.NetTiers.Data.Bases
{
    /// <summary>
    /// Serves as the base class for objects that provide data access functionality.
    /// Provides a default implementation of the IEntityProvider&lt;Entity, EntityKey&gt; interface.
    /// </summary>
    /// <typeparam name="Entity">The class of the business object being accessed.</typeparam>
    /// <typeparam name="EntityKey">The class of the EntityId
    /// property of the specified business object class.</typeparam>
    /// <remarks>
    /// This file is generated once and will never be overwritten.
    /// </remarks>
    [Serializable]
    [CLSCompliant(true)]
    public abstract partial class EntityProviderBase<Entity, EntityKey> : EntityProviderBaseCore<Entity, EntityKey>
        where Entity : IEntityId<EntityKey>, new()
        where EntityKey : IEntityKey, new()
    {

        /// <remarks>
        /// this intercepts all data requests
        /// if the command is an insert or update then it sets the created/updated dates, ids and names
        /// http://nettiers.com/InterceptEntityBeforeSaveUpdateDelete.ashx
        /// </remarks>
        protected override void OnDataRequesting(CommandEventArgs e)
        {
            base.OnDataRequesting(e);

            if (e.MethodName.Equals("Insert", StringComparison.CurrentCultureIgnoreCase))
            {
                UpdateCreatedData(e.CurrentEntity, e);
                UpdateUpdatedData(e.CurrentEntity, e);
            }
            else if (e.MethodName.Equals("Update", StringComparison.CurrentCultureIgnoreCase))
            {
                UpdateUpdatedData(e.CurrentEntity, e);
            }

            if (e.CurrentEntity is BbPatient)
            {
                // we have a patient
                if (e.MethodName.Equals("Insert", StringComparison.CurrentCultureIgnoreCase))
                {
                    // doing an update
                    EncryptPatient(e.CurrentEntity, e, false);
                }
                else if (e.MethodName.Equals("Update", StringComparison.CurrentCultureIgnoreCase))
                {
                    // doing an update
                    EncryptPatient(e.CurrentEntity, e, true);
                }
            }

            if (e.CurrentEntity is BbPatientLifestyle)
            {
                // we have a patient
                if (e.MethodName.Equals("Insert", StringComparison.CurrentCultureIgnoreCase))
                {
                    // doing an update
                    EncryptPatientLifestyle(e.CurrentEntity, e, false);
                }
                else if (e.MethodName.Equals("Update", StringComparison.CurrentCultureIgnoreCase))
                {
                    // doing an update
                    EncryptPatientLifestyle(e.CurrentEntity, e, true);
                }
            }
        }

        private void EncryptPatient(Object entity, CommandEventArgs e, bool IsUpdate)
        {
            try
            {
                bbCrypto myCrypto = new bbCrypto();
                string unencryptedVal;
                string encryptedVal;

                Type type = entity.GetType();

                string[] arrFieldsToEncrypt = { "Phrn", "Pnhs", "Title", "Surname", "Forenames", "Address1", "Address2", "Address3", "AddressTown", "AddressCounty", "AddressPostcode", "Phone", "Emailaddress", "Countryresidence" };
                BbPatient originalPatient = ((BbPatient)entity).GetOriginalEntity();
                string originalValue;

                foreach (string FieldToEncrypt in arrFieldsToEncrypt)
                {
                    PropertyInfo ThisProp = type.GetProperty(FieldToEncrypt);
                    if (ThisProp != null && ThisProp.CanWrite)
                    {
                        originalValue = null;
                        unencryptedVal = null;

                        if (null != ThisProp.GetValue(originalPatient, null)) originalValue = ThisProp.GetValue(originalPatient, null).ToString();
                        if (null != ThisProp.GetValue(entity, null)) unencryptedVal = ThisProp.GetValue(entity, null).ToString();
                        if (originalValue != unencryptedVal || false == IsUpdate)
                        {
                            encryptedVal = myCrypto.encrypt(unencryptedVal);
                            ThisProp.SetValue(entity, encryptedVal, null);
                            e.Command.Parameters["@" + FieldToEncrypt].Value = encryptedVal;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("problem encrypting patient", ex);
            }
        }

        private void EncryptPatientLifestyle(Object entity, CommandEventArgs e, bool IsUpdate)
        {
            try
            {
                bbCrypto myCrypto = new bbCrypto();
                string unencryptedVal;
                string encryptedVal;

                Type type = entity.GetType();

                string[] arrFieldsToEncrypt = { "Birthtown", "Birthcountry" };
                BbPatientLifestyle originalPatientLifestyle = ((BbPatientLifestyle)entity).GetOriginalEntity();
                string originalValue;

                foreach (string FieldToEncrypt in arrFieldsToEncrypt)
                {
                    PropertyInfo ThisProp = type.GetProperty(FieldToEncrypt);
                    if (ThisProp != null && ThisProp.CanWrite)
                    {
                        originalValue = null;
                        unencryptedVal = null;

                        if (null != ThisProp.GetValue(originalPatientLifestyle, null)) originalValue = ThisProp.GetValue(originalPatientLifestyle, null).ToString();
                        if (null != ThisProp.GetValue(entity, null)) unencryptedVal = ThisProp.GetValue(entity, null).ToString();
                        if (originalValue != unencryptedVal || false == IsUpdate)
                        {
                            encryptedVal = myCrypto.encrypt(unencryptedVal);
                            ThisProp.SetValue(entity, encryptedVal, null);
                            e.Command.Parameters["@" + FieldToEncrypt].Value = encryptedVal;
                        }
                    }
                }
            }
            catch { }
        }

        private void UpdateCreatedData(Object entity, CommandEventArgs e)
        {
            try
            {
                Type type = entity.GetType();

                HttpContext ctx = HttpContext.Current;
                if (ctx == null) { return; }

                PropertyInfo CreateddateProp = type.GetProperty("Createddate");
                if (CreateddateProp != null && CreateddateProp.CanWrite)
                {
                    DateTime now = DateTime.Now;
                    CreateddateProp.SetValue(entity, now, null);
                    e.Command.Parameters["@Createddate"].Value = now;
                }
                PropertyInfo CreatedbyidProp = type.GetProperty("Createdbyid");
                if (CreatedbyidProp != null && CreatedbyidProp.CanWrite)
                {
                    int UserBADBIRuserid = Convert.ToInt32(ctx.Session["UserBADBIRuserid"].ToString());
                    CreatedbyidProp.SetValue(entity, UserBADBIRuserid, null);
                    e.Command.Parameters["@Createdbyid"].Value = UserBADBIRuserid;
                }
                PropertyInfo CreatedbynameProp = type.GetProperty("Createdbyname");
                if (CreatedbynameProp != null && CreatedbyidProp.CanWrite)
                {
                    string UserFullname = ctx.Session["UserFullname"].ToString();
                    CreatedbynameProp.SetValue(entity, UserFullname, null);
                    e.Command.Parameters["@Createdbyname"].Value = UserFullname;
                }
            }
            catch { }
        }

        private void UpdateUpdatedData(Object entity, CommandEventArgs e)
        {
            try
            {
                Type type = entity.GetType();

                HttpContext ctx = HttpContext.Current;

                PropertyInfo LastupdateddateProp = type.GetProperty("Lastupdateddate");
                if (LastupdateddateProp != null && LastupdateddateProp.CanWrite)
                {
                    DateTime now = DateTime.Now;
                    LastupdateddateProp.SetValue(entity, now, null);
                    e.Command.Parameters["@Lastupdateddate"].Value = now;
                }
                PropertyInfo LastupdatedbyidProp = type.GetProperty("Lastupdatedbyid");
                if (LastupdatedbyidProp != null && LastupdatedbyidProp.CanWrite)
                {
                    int UserBADBIRuserid = Convert.ToInt32(ctx.Session["UserBADBIRuserid"].ToString());
                    LastupdatedbyidProp.SetValue(entity, UserBADBIRuserid, null);
                    e.Command.Parameters["@Lastupdatedbyid"].Value = UserBADBIRuserid;
                }
                PropertyInfo LastupdatedbynameProp = type.GetProperty("Lastupdatedbyname");
                if (LastupdatedbynameProp != null && LastupdatedbyidProp.CanWrite)
                {
                    string UserFullname = ctx.Session["UserFullname"].ToString();
                    LastupdatedbynameProp.SetValue(entity, UserFullname, null);
                    e.Command.Parameters["@Lastupdatedbyname"].Value = UserFullname;
                }
            }
            catch
            {
            }
        }
    }
}
