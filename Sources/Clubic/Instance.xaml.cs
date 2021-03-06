﻿//*******************************************************************************************************************************
// DEBUT DU FICHIER
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// Nom           : Instance.xaml.cs
// Auteur        : Nicolas Dagnas
// Description   : Implémentation du point d'entrée de l'application
// Créé le       : 17/02/2015
// Modifié le    : 13/05/2015
//*******************************************************************************************************************************

//-------------------------------------------------------------------------------------------------------------------------------
#region Using directives
//-------------------------------------------------------------------------------------------------------------------------------
using System;
using System.Windows;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Windows.Navigation;
using System.Collections.Generic;
using System.Windows.Phone.Infos;
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
using Microsoft.Phone.Shell;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Scheduler;
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
using Clubic.Panels;
using Clubic.Service;
using Clubic.Scheduler;
//-------------------------------------------------------------------------------------------------------------------------------
#endregion
//-------------------------------------------------------------------------------------------------------------------------------

//*******************************************************************************************************************************
// Début du bloc "Clubic"
//*******************************************************************************************************************************
namespace Clubic
	{

	//  #  #   #   ####  #####   ###   #   #   ###   #####
	//  #  ##  #  #        #    #   #  ##  #  #   #  #    
	//  #  # # #   ###     #    #####  # # #  #      ###  
	//  #  #  ##      #    #    #   #  #  ##  #   #  #    
	//  #  #   #  ####     #    #   #  #   #   ###   #####
	
	//***************************************************************************************************************************
	// Classe Instance
	//***************************************************************************************************************************
	#region // Déclaration et Implémentation de l'Objet
	//---------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Définit le poitn d'entrée de l'application.
	/// </summary>
	//---------------------------------------------------------------------------------------------------------------------------
	public partial class Instance : Application
		{
		//-----------------------------------------------------------------------------------------------------------------------
		// Section des Attributs
		//-----------------------------------------------------------------------------------------------------------------------
		private bool DisableFastResume           = false;
		private bool PhoneApplicationInitialized = false;
		//-----------------------------------------------------------------------------------------------------------------------

		//***********************************************************************************************************************
		#region // Section des Procédures Constructeurs
		//-----------------------------------------------------------------------------------------------------------------------

		//***********************************************************************************************************************
		/// <summary>
		/// Initialise une nouvelle instance de l'objet <b>Instance</b>.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public Instance ()
			{
			//-------------------------------------------------------------------------------------------------------------------
			this.UnhandledException += this.OnUnhandledException;

			this.InitializeComponent        ();
			this.InitializePhoneApplication ();
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			// Mise à jour ?
			//-------------------------------------------------------------------------------------------------------------------
			string Version = StorageSettings.GetValue ( "version", string.Empty );

			if ( ! Version.Equals ( VersionUtils.Current.ToString () ) )
				{
				//---------------------------------------------------------------------------------------------------------------
				System.IO.AppWebCache.Clear ();

				StorageSettings.SetValue ( "version"   , VersionUtils.Current.ToString () );
				StorageSettings.SetValue ( "panel-mode", (int)PanelMode.Popup             );
				//---------------------------------------------------------------------------------------------------------------
				}
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			// Gestion de l'Agent
			//-------------------------------------------------------------------------------------------------------------------
			Instance.CheckScheduledAgentStatus ();
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			if ( Debugger.IsAttached )
				{
				//---------------------------------------------------------------------------------------------------------------
				Application.Current.Host.Settings.EnableFrameRateCounter = false;
				//---------------------------------------------------------------------------------------------------------------

				//---------------------------------------------------------------------------------------------------------------
				PhoneApplicationService.Current.ApplicationIdleDetectionMode = IdleDetectionMode.Enabled;
				PhoneApplicationService.Current.UserIdleDetectionMode        = IdleDetectionMode.Enabled;
				//---------------------------------------------------------------------------------------------------------------
				}
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//-----------------------------------------------------------------------------------------------------------------------
		#endregion
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		#region // Section des Procédures Privées
		//-----------------------------------------------------------------------------------------------------------------------

		//***********************************************************************************************************************
		/// <summary>
		/// Initialise l'application.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		private void InitializePhoneApplication ()
			{
			//-------------------------------------------------------------------------------------------------------------------
			if ( this.PhoneApplicationInitialized ) return;
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			Instance.RootFrame = new TransitionFrame ();

			Instance.RootFrame.Navigated        += this.CompleteInitializePhoneApplication;
			Instance.RootFrame.NavigationFailed += this.OnNavigationFailed;
			Instance.RootFrame.Navigating       += this.DisablingFastResume;
			Instance.RootFrame.Navigated        += this.CheckForResetNavigation;
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			this.PhoneApplicationInitialized = true;
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Complète l'initialisation.
		/// </summary>
		/// <param name="Sender">Objet source de l'appel.</param>
		/// <param name="Args">
		/// <b>NavigationEventArgs</b> qui contient les données d'événement.
		/// </param>
		//-----------------------------------------------------------------------------------------------------------------------
		private void CheckForResetNavigation ( object Sender, NavigationEventArgs Args )
			{
			//-------------------------------------------------------------------------------------------------------------------
			if ( Args.NavigationMode == NavigationMode.Reset )
				Instance.RootFrame.Navigated += this.ClearBackStackAfterReset;
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Complète l'initialisation.
		/// </summary>
		/// <param name="Sender">Objet source de l'appel.</param>
		/// <param name="Args">
		/// <b>NavigationEventArgs</b> qui contient les données d'événement.
		/// </param>
		//-----------------------------------------------------------------------------------------------------------------------
		private void CompleteInitializePhoneApplication ( object Sender, NavigationEventArgs Args )
			{
			//-------------------------------------------------------------------------------------------------------------------
			if ( this.RootVisual != Instance.RootFrame ) this.RootVisual = Instance.RootFrame;

			Instance.RootFrame.Navigated -= this.CompleteInitializePhoneApplication;
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			if ( Args.NavigationMode == NavigationMode.Reset )
				Instance.RootFrame.Navigated += this.ClearBackStackAfterReset;
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Complète la désactivation du Fast Resume.
		/// </summary>
		/// <param name="Sender">Objet source de l'appel.</param>
		/// <param name="Args">
		/// <b>NavigatingCancelEventArgs</b> qui contient les données d'événement.
		/// </param>
		//-----------------------------------------------------------------------------------------------------------------------
		private void DisablingFastResume ( object Sender, NavigatingCancelEventArgs Args )
			{
			//-------------------------------------------------------------------------------------------------------------------
			if ( Args.NavigationMode == NavigationMode.Reset )
				{ Args.Cancel = true; this.DisableFastResume = true;  }
			//-------------------------------------------------------------------------------------------------------------------
			else if ( this.DisableFastResume )
				{ Args.Cancel = true; this.DisableFastResume = false; }
			//-------------------------------------------------------------------------------------------------------------------
			else                    { this.DisableFastResume = false; }
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Complète l'initialisation.
		/// </summary>
		/// <param name="Sender">Objet source de l'appel.</param>
		/// <param name="Args">
		/// <b>NavigationEventArgs</b> qui contient les données d'événement.
		/// </param>
		//-----------------------------------------------------------------------------------------------------------------------
		private void ClearBackStackAfterReset ( object Sender, NavigationEventArgs Args )
			{
			//-------------------------------------------------------------------------------------------------------------------
			Instance.RootFrame.Navigated -= this.ClearBackStackAfterReset;

			if ( Args.NavigationMode != NavigationMode.New && Args.NavigationMode != NavigationMode.Refresh ) return;

			while ( RootFrame.RemoveBackEntry () != null ) {}
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//-----------------------------------------------------------------------------------------------------------------------
		#endregion
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		#region // Section des Evenements Application
		//-----------------------------------------------------------------------------------------------------------------------

		//***********************************************************************************************************************
		/// <summary>
		/// Se produit quand l'application se charge.
		/// </summary>
		/// <param name="Sender">Objet source de l'appel.</param>
		/// <param name="Args">
		/// <b>LaunchingEventArgs</b> qui contient les données d'événement.
		/// </param>
		//-----------------------------------------------------------------------------------------------------------------------
		private void OnLaunching ( object Sender, LaunchingEventArgs Args ) {}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Se produit quand l'application est activée.
		/// </summary>
		/// <param name="Sender">Objet source de l'appel.</param>
		/// <param name="Args">
		/// <b>ActivatedEventArgs</b> qui contient les données d'événement.
		/// </param>
		//-----------------------------------------------------------------------------------------------------------------------
		private void OnActivated ( object Sender, ActivatedEventArgs Args ) {}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Se produit quand l'application est désactivée.
		/// </summary>
		/// <param name="Sender">Objet source de l'appel.</param>
		/// <param name="Args">
		/// <b>DeactivatedEventArgs</b> qui contient les données d'événement.
		/// </param>
		//-----------------------------------------------------------------------------------------------------------------------
		private void OnDeactivated ( object Sender, DeactivatedEventArgs Args ) {}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Se produit quand l'application se ferme.
		/// </summary>
		/// <param name="Sender">Objet source de l'appel.</param>
		/// <param name="Args">
		/// <b>ClosingEventArgs</b> qui contient les données d'événement.
		/// </param>
		//-----------------------------------------------------------------------------------------------------------------------
		private void OnClosing ( object Sender, ClosingEventArgs Args )
			{
			//-------------------------------------------------------------------------------------------------------------------
			#region // DEBUG
			//-------------------------------------------------------------------------------------------------------------------
#if DEBUG
			//-------------------------------------------------------------------------------------------------------------------
			if ( Instance.ScheduledAgentActive )
				ScheduledActionService.LaunchForTest ( AppInfos.SchedulerName, TimeSpan.FromSeconds (10) );
			//-------------------------------------------------------------------------------------------------------------------
#endif
			//-------------------------------------------------------------------------------------------------------------------
			#endregion
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Se produit quand une exception est générée.
		/// </summary>
		/// <param name="Sender">Objet source de l'appel.</param>
		/// <param name="Args">
		/// <b>ApplicationUnhandledExceptionEventArgs</b> qui contient les données d'événement.
		/// </param>
		//-----------------------------------------------------------------------------------------------------------------------
		private void OnUnhandledException ( object Sender, 
		                                              ApplicationUnhandledExceptionEventArgs Args )
			{
			//-------------------------------------------------------------------------------------------------------------------
			#region // DEBUG
			//-------------------------------------------------------------------------------------------------------------------
#if DEBUG
			//-------------------------------------------------------------------------------------------------------------------
			if ( Debugger.IsAttached ) { Debugger.Break (); return; }
			//-------------------------------------------------------------------------------------------------------------------
#endif
			//-------------------------------------------------------------------------------------------------------------------
			#endregion
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//-----------------------------------------------------------------------------------------------------------------------
		#endregion
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		#region // Section des Evenements RootFrame
		//-----------------------------------------------------------------------------------------------------------------------

		//***********************************************************************************************************************
		/// <summary>
		/// Se produit quand une navigation échoue.
		/// </summary>
		/// <param name="Sender">Objet source de l'appel.</param>
		/// <param name="Args">
		/// <b>NavigationFailedEventArgs</b> qui contient les données d'événement.
		/// </param>
		//-----------------------------------------------------------------------------------------------------------------------
		private void OnNavigationFailed ( object Sender, NavigationFailedEventArgs Args )
			{
			//-------------------------------------------------------------------------------------------------------------------
			#region // DEBUG
			//-------------------------------------------------------------------------------------------------------------------
#if DEBUG
			//-------------------------------------------------------------------------------------------------------------------
			if ( Debugger.IsAttached ) { Debugger.Break (); return; }
			//-------------------------------------------------------------------------------------------------------------------
#endif
			//-------------------------------------------------------------------------------------------------------------------
			#endregion
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		
		//-----------------------------------------------------------------------------------------------------------------------
		#endregion
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		#region // Section des Analyseurs
		//-----------------------------------------------------------------------------------------------------------------------
		
		//***********************************************************************************************************************
		/// <summary>
		/// Informe le service Google qu'une vue a été chargée.
		/// </summary>
		/// <param name="Address">Url visitée.</param>
		//-----------------------------------------------------------------------------------------------------------------------
		public static void SendGaView ( RestEventArgs Self, string ViewName, params object[] Args )
			{
			//-------------------------------------------------------------------------------------------------------------------
#if ! DEBUG
			//-------------------------------------------------------------------------------------------------------------------
			if ( Self.Result != RestRequestResult.Success )
				{
				//---------------------------------------------------------------------------------------------------------------
				List<object> _Args = new List<object> ( Args );

				_Args.Add ( Self.Result.ToString () );
					
				Args = _Args.ToArray ();
				//---------------------------------------------------------------------------------------------------------------
				}
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			Instance.GAnalytics.SendView ( ViewName, Args );
			//-------------------------------------------------------------------------------------------------------------------
#endif
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		
		//-----------------------------------------------------------------------------------------------------------------------
		#endregion
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		#region // Section de Gestion de l'Agent
		//-----------------------------------------------------------------------------------------------------------------------
		
		//***********************************************************************************************************************
		/// <summary>
		/// Contrôle le statut de l'agent.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public static void CheckScheduledAgentStatus ()
			{
			//-------------------------------------------------------------------------------------------------------------------
			try
				{
				//---------------------------------------------------------------------------------------------------------------
				foreach ( var Task in ScheduledActionService.GetActions<PeriodicTask> () )
					{
					ScheduledActionService.Remove ( Task.Name );
					}
				//---------------------------------------------------------------------------------------------------------------
				}
			//-------------------------------------------------------------------------------------------------------------------
			catch {}
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			try
				{
				//---------------------------------------------------------------------------------------------------------------
				Instance.ScheduledAgentActive = false;

				if ( ScheduledAgent.TileIsActive || ScheduledAgent.ToastDelay > 0 )
					{
					var PeriodicTask = new PeriodicTask ( AppInfos.SchedulerName );

					PeriodicTask.Description = AppInfos.SchedulerDescription;

					ScheduledActionService.Add ( PeriodicTask );

					Instance.ScheduledAgentActive = PeriodicTask.IsEnabled && 
					                                PeriodicTask.IsScheduled;
					}
				//---------------------------------------------------------------------------------------------------------------
				}
			//-------------------------------------------------------------------------------------------------------------------
			catch {}
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//-----------------------------------------------------------------------------------------------------------------------
		#endregion
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		#region // >> ExpandMenu
		//-----------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Indique si le menu doit rester visible.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public static bool ExpandMenu
			{
			//-------------------------------------------------------------------------------------------------------------------
			get { return (bool)StorageSettings.GetValue ( "expand-menu", false       ); }
			set {              StorageSettings.SetValue ( "expand-menu", (bool)value ); }
			//-------------------------------------------------------------------------------------------------------------------
			}
		//-----------------------------------------------------------------------------------------------------------------------
		#endregion
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		#region // >> PanelMode
		//-----------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Obtiens ou définit le mode d'affichage des Sections.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public static PanelMode PanelMode
			{
			//-------------------------------------------------------------------------------------------------------------------
			get
				{
				//---------------------------------------------------------------------------------------------------------------
				if ( DeviceInfos.Version == WindowsPhoneVersion.WP10 ) return PanelMode.Popup;

				return (PanelMode)StorageSettings.GetValue ( "panel-mode", (int)PanelMode.Popup );
				//---------------------------------------------------------------------------------------------------------------
				}
			//-------------------------------------------------------------------------------------------------------------------
			set { StorageSettings.SetValue ( "panel-mode", (int)value ); }
			//-------------------------------------------------------------------------------------------------------------------
			}
		//-----------------------------------------------------------------------------------------------------------------------
		#endregion
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Obbtiens l'objet <b>PhoneApplicationFrame</b> de l'application.
		/// </summary>
		/// <returns>Objet <b>PhoneApplicationFrame</b>.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public static PhoneApplicationFrame RootFrame { get; private set; }
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Indique si l'agent de fond est actif.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public static bool ScheduledAgentActive { get; private set; }
		//***********************************************************************************************************************
		}
	//---------------------------------------------------------------------------------------------------------------------------
	#endregion
	//***************************************************************************************************************************

	} // Fin du namespace "Clubic"
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// FIN DU FICHIER
//*******************************************************************************************************************************
