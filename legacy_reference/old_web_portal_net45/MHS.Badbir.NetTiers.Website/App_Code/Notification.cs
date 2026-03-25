using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Notification
/// </summary>
/// 

namespace MHS.Badbir.NetTiers
{
    /*
     * Class to generate notifications for various database actions that need to be monitored and checked.
     * 
     * Patient-related notifications can be generated using generateNotificationFromPatient()
     * 
     * 25/11/2019 Added failsafe notification generation in the case of an exception. States the exception details in the notification body. 
     * 
     */
    public static class Notification
    {
        public static bool defaultPriority = false; // All notifications are normal priority by default
        public static bool defaultUnread = true;    // All notifications start as unread
        public const byte adminInboxID = 1;         // The Inbox ID of the admin inbox
        public const byte pvInboxID = 2;            // The Inbox ID of the PV inbox
        public const byte supInboxID = 3;           // The Inbox ID of the Super inbox

        /*
         * Function to generate a notification for a given inbox. 
         * 
         * inboxID - corresponds to the target inbox: 1 = admin; 2 = PV; 3 = super
         * typeID - the type of notification, links to bbNotificationTypelkp
         * groupID - optional, used to group single send-outs of notifications (e.g. a news item to multiple users)
         * chid - optional, if relating to a patient, use their chid, which identifies the cohort as well as their study number
         * bbUserID - optional, if sending to a particular user, this is their BADBIR user account ID
         * highPriority - default is false, can be set to true to highlight this notification as high priority
         * nText - the body text of the notification
         */
        public static bool generateNotification(int inboxID, int typeID, int? groupID, int? chid, int? bbUserID, bool highPriority, String nText)
        {

            try
            {
                Entities.BbNotification notification = new Entities.BbNotification();

                notification.InboxId = Convert.ToByte(inboxID);
                notification.TypeId = Convert.ToByte(typeID);
                notification.GroupId = groupID;
                notification.Chid = chid;
                notification.Unread = defaultUnread; // Always unread
                notification.HighPriority = highPriority;
                notification.Ntext = nText;

                Data.DataRepository.BbNotificationProvider.Save(notification);

                // Success
                return true;
            }
            catch (Exception e)
            {
                // Failed somehow
                Entities.BbNotification notification = new Entities.BbNotification();

                notification.InboxId = 3;                           // Always Super
                notification.TypeId = 7;                            // Notification error
                notification.Unread = defaultUnread;                // Always unread
                notification.HighPriority = true;                   // Always high priority
                notification.Ntext = "An error occurred when generating a notification. Technical details: Exception in generateNotification(): " + e.Message;

                Data.DataRepository.BbNotificationProvider.Save(notification);

                return false;
            }
        }

        public static bool generateNotificationFromPatient(int inboxID, int typeID, int chid, bool highPriority, String nText)
        {
            try
            {
                Entities.BbNotification notification = new Entities.BbNotification();

                notification.InboxId = Convert.ToByte(inboxID);
                notification.TypeId = Convert.ToByte(typeID);
                notification.Chid = chid;
                notification.Unread = defaultUnread; // Always unread
                notification.HighPriority = highPriority;
                notification.Ntext = nText;

                MHS.Badbir.NetTiers.Data.DataRepository.BbNotificationProvider.Save(notification);

                // Success
                return true;
            } catch (Exception e)
            {
                // Failed somehow
                Entities.BbNotification notification = new Entities.BbNotification();

                notification.InboxId = 3;                           // Always Super
                notification.TypeId = 7;                            // Notification error
                notification.Unread = defaultUnread;                // Always unread
                notification.HighPriority = true;                   // Always high priority
                notification.Ntext = "An error occurred when generating a notification. Technical details: Exception in generateNotificationFromPatient(): " + e.Message;

                Data.DataRepository.BbNotificationProvider.Save(notification);

                return false;
            }
        }




        /* Function to quickly generate an error message to the Super inbox, passing the notification text only */
        public static bool sendSuperNotifyError(String nText)
        {
            try
            {
                Entities.BbNotification notification = new Entities.BbNotification();

                notification.InboxId = supInboxID;
                notification.TypeId = 10;    // Database error
                notification.GroupId = null;
                notification.Chid = null;
                notification.Unread = defaultUnread; // Always unread
                notification.HighPriority = defaultPriority;
                notification.Ntext = nText;

                Data.DataRepository.BbNotificationProvider.Save(notification);

                // Success
                return true;
            }
            catch (Exception e)
            {
                // Failed somehow
                Entities.BbNotification notification = new Entities.BbNotification();

                notification.InboxId = 3;                           // Always Super
                notification.TypeId = 7;                            // Notification error
                notification.Unread = defaultUnread;                // Always unread
                notification.HighPriority = true;                   // Always high priority
                notification.Ntext = "An error occurred when generating a notification. Technical details: Exception in sendSuperNotifyError(): " + e.Message;

                Data.DataRepository.BbNotificationProvider.Save(notification);

                return false;
            }
        }

        /* Function to quickly generate a navigation tracking message to the Super inbox, passing the notification text only */
        public static bool sendSuperNotifyNavTrack(String nText)
        {
            try
            {
                Entities.BbNotification notification = new Entities.BbNotification();

                notification.InboxId = supInboxID;
                notification.TypeId = 11;    // Navigation tracking
                notification.GroupId = null;
                notification.Chid = null;
                notification.Unread = defaultUnread; // Always unread
                notification.HighPriority = defaultPriority;
                notification.Ntext = nText;

                Data.DataRepository.BbNotificationProvider.Save(notification);

                // Success
                return true;
            }
            catch (Exception e)
            {
                // Failed somehow
                Entities.BbNotification notification = new Entities.BbNotification();

                notification.InboxId = 3;                           // Always Super
                notification.TypeId = 7;                            // Notification error
                notification.Unread = defaultUnread;                // Always unread
                notification.HighPriority = true;                   // Always high priority
                notification.Ntext = "An error occurred when generating a notification. Technical details: Exception in sendSuperNotifyError(): " + e.Message;

                Data.DataRepository.BbNotificationProvider.Save(notification);

                return false;
            }
        }

        /*
         *  Generate a new notification into the specified inbox, from a patient (requiring a chid)
         *  typeID - the notification type (like query type) - see bbNotificationTypelkp table for details
         *  chid - the cohort history ID of the patient
         *  highPriority - default is false; true for important notifications that should be actioned first
         *  nText - the body text of the notification
         */
        public static bool newNotificationFromPatient(byte inboxID, byte typeID, int chid, bool highPriority, String nText)
        {
            try
            {
                Entities.BbNotification notification = new Entities.BbNotification();

                notification.InboxId = inboxID;         // Specify inbox from the constants in the class
                notification.TypeId = typeID;           // As a byte
                notification.Chid = chid;
                notification.Unread = defaultUnread;    // Always unread
                notification.HighPriority = highPriority;
                notification.Ntext = nText;

                Data.DataRepository.BbNotificationProvider.Save(notification);

                // Success
                return true;
            }
            catch (Exception e)
            {
                // Failed somehow
                Entities.BbNotification notification = new Entities.BbNotification();

                notification.InboxId = 3;                           // Always Super
                notification.TypeId = 7;                            // Notification error
                notification.Unread = defaultUnread;                // Always unread
                notification.HighPriority = true;                   // Always high priority
                notification.Ntext = "An error occurred when generating a notification. Technical details: Exception in newNotificationFromPatient(): " + e.Message;

                Data.DataRepository.BbNotificationProvider.Save(notification);

                return false;
            }
        }


        /*
         *  Generate a new notification into the specified inbox
         *  typeID - the notification type (like query type) - see bbNotificationTypelkp table for details
         *  highPriority - default is false; true for important notifications that should be actioned first
         *  nText - the body text of the notification
         */
        public static bool newNotification(byte inboxID, byte typeID, bool highPriority, String nText)
        {
            try
            {
                Entities.BbNotification notification = new Entities.BbNotification();

                notification.InboxId = inboxID;         // Specify inbox from the constants in the class
                notification.TypeId = typeID;           // As a byte
                notification.Unread = defaultUnread;    // Always unread
                notification.HighPriority = highPriority;
                notification.Ntext = nText;

                Data.DataRepository.BbNotificationProvider.Save(notification);

                // Success
                return true;
            }
            catch (Exception e)
            {
                // Failed somehow
                Entities.BbNotification notification = new Entities.BbNotification();

                notification.InboxId = 3;                           // Always Super
                notification.TypeId = 7;                            // Notification error
                notification.Unread = defaultUnread;                // Always unread
                notification.HighPriority = true;                   // Always high priority
                notification.Ntext = "An error occurred when generating a notification. Technical details: Exception in newNotification(): " + e.Message;

                Data.DataRepository.BbNotificationProvider.Save(notification);
                return false;
            }
        }


    }
}
