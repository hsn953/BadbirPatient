<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/bbDefault.master" AutoEventWireup="true" CodeFile="ChangeLog.aspx.cs" Inherits="Account_ChangeLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript">
        $(document).ready(function() 
        {
            $('#previousIterations').siblings().hide(); 
            $('#previousIterations').html('Previous Iterations <i class="fas fa-caret-down fa-fw"></i>');
        
            $('#previousIterations').click(function(){
                if($('#previousIterations').html()=='Previous Iterations <i class="fas fa-caret-down fa-fw"></i>')
                {
                    $('#previousIterations').html('Previous Iterations <i class="fas fa-caret-up fa-fw"></i>');
                    $('#previousIterations').siblings().show(250); 
                }
                else
                {
                    $('#previousIterations').html('Previous Iterations <i class="fas fa-caret-down fa-fw"></i>');
                    $('#previousIterations').siblings().hide(250); 
                }    
            });
        });
    </script>

    <style type="text/css">

        .changesPage {
            max-width: 960px;
            background-color: #f7f7f7;
            padding: 10px;
            border-radius: 30px;
            border: 10px solid #d5d5d5;
        }

        .changesTitle {
            font-size: 250%;
            color: #676767;
            font-family: 'Trebuchet MS', sans-serif;
        }

        .changesSubtitle {
            color: #676767;
            font-size: 110%;
        }

        .changeList {
            margin-left: 15px;
            font-size: 120%;
            list-style: none;
            line-height: 25px;
        }

        .changeListItem {
            list-style: none;
            padding: 5px 0px;
        }

        .innerChangeList {
            margin: 0;
            margin-top: -5px;
            margin-left: 30px;
            list-style: none;
        }

        .innerChangeListItem {
            list-style: none;
        }

        fieldset {
            padding-left: 10px;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h1 class="changesTitle">BADBIR Database Changes</h1>
    <div class="changesPage">
        <h2><asp:Label runat="server" ID="lblDatabaseRevision"></asp:Label></h2>
    
        <div runat="server" id="divClinicians" visible="false">
            <p class="changesSubtitle">
                If you have any issues or feedback on the changes in this update, please <a href="mailto:oliver.steer@manchester.ac.uk?subject=Database%20Changes">get in touch via email</a>. 
            </p>
            <ul class="changeList">
                <li class="changeListItem"><i class="fas fa-chevron-circle-right fa-fw"></i> Updated the consent form version field to allow decimal numbers</li>
                <li class="changeListItem"><i class="fas fa-chevron-circle-right fa-fw"></i> Updated some date validation ranges to help prevent erroneous dates from being entered</li>
                <li class="changeListItem"><i class="fas fa-chevron-circle-right fa-fw"></i> Bug fixes and general improvements</li>
            </ul>
        </div>
    
        <div runat="server" id="divAdmins" visible="false">
            <h2>Changes affecting Admins</h2>
            <p class="changesSubtitle">
                Please see the change request files in the Change Management folder for more details. 
            </p>
            <ul class="changeList">
                <li class="changeListItem">
                    <i class="fas fa-chevron-circle-right fa-fw"></i> 2020B-0001: Several minor updates and improvements to the user registration process
                    <ul class="innerChangeList">
                        <li class="innerChangeListItem">
                            <i class="fas fa-angle-right fa-fw"></i> Confirmation dialog added to the PI Approve/Reject buttons
                        </li>
                        <li class="innerChangeListItem">
                            <i class="fas fa-angle-right fa-fw"></i> Reduced training video resolution from 1280x720 to 1140x640, removing the horizontal scroll bar for 720p screens
                        </li>
                        <li class="innerChangeListItem">
                            <i class="fas fa-angle-right fa-fw"></i> Login page now allows a new user to log in if they have a role, but still have a pre-registration record
                        </li>
                        <li class="innerChangeListItem">
                            <i class="fas fa-angle-right fa-fw"></i> Fixed the issue where training centres would not show in the user's centre list (works in normal &amp; training mode)
                        </li>
                    </ul>
                </li>
                <li class="changeListItem">
                    <i class="fas fa-chevron-circle-right fa-fw"></i> 2020B-0002: Added a new query flag to filter audit queries out of the normal inbox
                    <ul class="innerChangeList">
                        <li class="innerChangeListItem">
                            <i class="fas fa-angle-right fa-fw"></i> New inbox added in the <strong>Manage Users &amp Centres</strong> section
                        </li>
                        <li class="innerChangeListItem">
                            <i class="fas fa-angle-right fa-fw"></i> Queries will retain their type and status, with the additional flag available to separate audit queries
                        </li>
                        <li class="innerChangeListItem">
                            <i class="fas fa-angle-right fa-fw"></i> Fixed some bugs with the query search parameters, including an auto-reset if the search would cause an error
                        </li>
                        <li class="innerChangeListItem">
                            <i class="fas fa-angle-right fa-fw"></i> Note: No changes to how queries appear for clinicians
                        </li>
                    </ul>
                </li>
                <li class="changeListItem">
                    <i class="fas fa-chevron-circle-right fa-fw"></i> 2020B-0003: Validation updates across the system
                    <ul class="innerChangeList">
                        <li class="innerChangeListItem">
                            <i class="fas fa-angle-right fa-fw"></i> Date range validators on questionnaires aligned; minimum date is patient's DOB, maximum date is today
                        </li>
                        <li class="innerChangeListItem">
                            <i class="fas fa-angle-right fa-fw"></i> Drinking logic update: Selecting 'Yes' for drinking makes the CAGE applicable (but can be overridden)
                        </li>
                        <li class="innerChangeListItem">
                            <i class="fas fa-angle-right fa-fw"></i> 'First biologic' logic update: Checks previous therapy (if clinician has entered/confirmed) when saving biologic therapy at baseline
                        </li>
                        <li class="innerChangeListItem">
                            <i class="fas fa-angle-right fa-fw"></i> Fixed Small Molecule confirmation - this is now checked in the same way as biologic therapy, at baseline &amp; follow-up
                        </li>
                        <li class="innerChangeListItem">
                            <i class="fas fa-angle-right fa-fw"></i> Fixed lesions &amp; smoking validation/auto-setting logic
                        </li>
                    </ul>
                </li>
                <li class="changeListItem">
                    <i class="fas fa-chevron-circle-right fa-fw"></i> 2020B-0004: Removing withdrawn patients from external studies <i>(in progress)</i>
                    <ul class="innerChangeList">
                        <li class="innerChangeListItem">
                            <i class="fas fa-angle-right fa-fw"></i> Added a gridview on the <strong>Patient Admin</strong> page showing all external study links for that patient
                        </li>
                        <li class="innerChangeListItem">
                            <i class="fas fa-angle-right fa-fw"></i> Option to remove the external study link if a patient withdraws (will require admin SOP change)
                        </li>
                    </ul>
                </li>
                <li class="changeListItem">
                    <i class="fas fa-chevron-circle-right fa-fw"></i> 2020B-0005: Allow decimals in consent form version
                    <ul class="innerChangeList">
                        <li class="innerChangeListItem">
                            <i class="fas fa-angle-right fa-fw"></i> Consent form versions with decimals can now be entered
                        </li>
                        <li class="innerChangeListItem">
                            <i class="fas fa-angle-right fa-fw"></i> Updated consent date range validation from 01/01/1900-31/12/2020 to 01/08/2007-today (first patient registered on 08/08/2007)
                        </li>
                        <li class="innerChangeListItem">
                            <i class="fas fa-angle-right fa-fw"></i> No change to date validation on <strong>Patient Admin</strong> page; any date can be entered here
                        </li>
                    </ul>
                </li>
                <li class="changeListItem">
                    <i class="fas fa-chevron-circle-right fa-fw"></i> 2020B-0006: Logic change for switchers commencing a non-BADBIR drug
                    <ul class="innerChangeList">
                        <li class="innerChangeListItem">
                            <i class="fas fa-angle-right fa-fw"></i> Cohort will be paused until the patient commences a drug we recruit for
                        </li>
                        <li class="innerChangeListItem">
                            <i class="fas fa-angle-right fa-fw"></i> Admin Team can add a cohort end date to the cohort the patient is switching away from
                        </li>
                        <li class="innerChangeListItem">
                            <i class="fas fa-angle-right fa-fw"></i> If the patient is later switched, the gap between cohorts represents the time the patient was not on a BADBIR drug
                        </li>
                        <li class="innerChangeListItem">
                            <i class="fas fa-angle-right fa-fw"></i> Improvements to the cohort switch process
                        </li>
                    </ul>
                </li>

                <li class="changeListItem">
                    <i class="fas fa-chevron-circle-right fa-fw"></i> Other minor bug fixes and improvements
                    <ul class="innerChangeList">
                        <li class="innerChangeListItem">
                            <i class="fas fa-angle-right fa-fw"></i> Added scrollbar to PV inbox search filters, so all filters are visible on smaller screens
                        </li>
                        <li class="innerChangeListItem">
                            <i class="fas fa-angle-right fa-fw"></i> Patient search text box is now highlighted after a search - easily perform another search by typing a new study ID
                        </li>
                    </ul>
                </li>
            </ul>
            <fieldset>
                <legend id="previousIterations" style="cursor: pointer;">Previous Iterations</legend>
                <h2>2020A (April 2020)</h2>
                <div>
                    <h3>Clinicians</h3>
                    <ul class="changeList">
                        <li class="changeListItem"><i class="fas fa-chevron-circle-right fa-fw"></i> Fixed an issue where PIs received an error when approving new accounts</li>
                        <li class="changeListItem"><i class="fas fa-chevron-circle-right fa-fw"></i> Removed some count bubbles on the dashboard page, as they were inconsistent</li>
                        <li class="changeListItem"><i class="fas fa-chevron-circle-right fa-fw"></i> Increased maximum biologic dose allowed, from 300mg to 400mg, as Cimzia can be prescribed at 400mg</li>
                        <li class="changeListItem"><i class="fas fa-chevron-circle-right fa-fw"></i> Bug fixes and general improvements</li>
                    </ul>
                </div>
    
                <div>
                    <h2>Admins</h2>
                    <ul class="changeList">
                        <li class="changeListItem"><i class="fas fa-chevron-circle-right fa-fw"></i> 2020A-0003: Added validation against marking a follow-up as missed, if it contains an adverse event</li>
                        <li class="changeListItem">
                            <i class="fas fa-chevron-circle-right fa-fw"></i> 2020A-0004: When a clinician responds to a query with a linked AE, the status will now <strong>always</strong> change to 'Query responded' 
                            <ul class="innerChangeList"><li class="innerChangeListItem"><i class="fas fa-angle-right fa-fw"></i>Note: if an admin responds and ticks 'respond as clinician', the status is <strong>not</strong> changed</li></ul>
                        </li>
                        <li class="changeListItem">
                            <i class="fas fa-chevron-circle-right fa-fw"></i> 2020A-0006: Added search controls for solved PV notifications
                            <ul class="innerChangeList"><li class="innerChangeListItem"><i class="fas fa-angle-right fa-fw"></i>Can search within any notification inbox from this page</li></ul>
                        </li>
                        <li class="changeListItem">
                            <i class="fas fa-chevron-circle-right fa-fw"></i> 2020A-0010: SAE category changes are now disabled on carried forward adverse events
                            <ul class="innerChangeList"><li class="innerChangeListItem"><i class="fas fa-angle-right fa-fw"></i>Additional validation fixes have also been applied around event &amp; hospitalisation dates</li></ul>
                        </li>
                        <li class="changeListItem"><i class="fas fa-chevron-circle-right fa-fw"></i> 2020A-0011: MedDRA version updated from 21.0 to 23.0; migration of out-of-date codes is in progress</li>
                        <li class="changeListItem"><i class="fas fa-chevron-circle-right fa-fw"></i> Added validation against adding duplicate concomitant drugs</li>
                    </ul>
                </div>
            </fieldset>
        </div>
    </div>
</asp:Content>

