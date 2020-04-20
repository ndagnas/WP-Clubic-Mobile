﻿//*******************************************************************************************************************************
// DEBUT DU FICHIER
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// Nom           : LeftPanelColumnMode.xaml.cs
// Auteur        : Nicolas Dagnas
// Description   : Implémentation de la Page LeftPanelColumnMode
// Créé le       : 21/03/2015
// Modifié le    : 13/05/2015
//*******************************************************************************************************************************

//-------------------------------------------------------------------------------------------------------------------------------
#region Using directives
//-------------------------------------------------------------------------------------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Phone.Controls;
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
using Clubic.Service;
//-------------------------------------------------------------------------------------------------------------------------------
#endregion
//-------------------------------------------------------------------------------------------------------------------------------

//*******************************************************************************************************************************
// Début du bloc "Clubic.Panels"
//*******************************************************************************************************************************
namespace Clubic.Panels
	{

	//  #      #####  #####  #####         ####    ###   #   #  #####  #    
	//  #      #      #        #           #   #  #   #  ##  #  #      #    
	//  #      ###    ###      #    #####  ####   #####  # # #  ###    #    
	//  #      #      #        #           #      #   #  #  ##  #      #    
	//  #####  #####  #        #           #      #   #  #   #  #####  #####

	//***************************************************************************************************************************
	// Contrôle LeftPanelColumnMode
	//***************************************************************************************************************************
	#region // Déclaration et Implémentation de l'Objet
	//---------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Définit la page permettant l'identification.
	/// </summary>
	//---------------------------------------------------------------------------------------------------------------------------
	public partial class LeftPanelColumnMode : UserControl
		{
		//***********************************************************************************************************************
		/// <summary>
		/// Initialise une nouvelle instance de l'objet <b>LeftPanelColumnMode</b>.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public LeftPanelColumnMode () { this.InitializeComponent (); }
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Est appelé lors d'un clic sur un lien interne.
		/// </summary>
		/// <param name="Sender">Objet source de l'appel.</param>
		/// <param name="Args"><b>RoutedEventArgs</b> qui contient les données d'événement.</param>
		//-----------------------------------------------------------------------------------------------------------------------
		private void OnSectionClick ( object Sender, RoutedEventArgs Args )
			{
			//-------------------------------------------------------------------------------------------------------------------
			FrameworkElement Self = Sender as FrameworkElement;

			if ( Self != null && this.Click != null )
				{
				//---------------------------------------------------------------------------------------------------------------
				SectionType Section = SectionType.All;

				switch ( Self.Tag.ToString () )
					{
					case "SectionType.Mobility"   : Section = SectionType.Mobility;   break;
					case "SectionType.Internet"   : Section = SectionType.Internet;   break;
					case "SectionType.Multimedia" : Section = SectionType.Multimedia; break;
					case "SectionType.Technology" : Section = SectionType.Technology; break;
					case "SectionType.Mac"        : Section = SectionType.Mac;        break;
					case "SectionType.Hardware"   : Section = SectionType.Hardware;   break;
					case "SectionType.Pro"        : Section = SectionType.Pro;        break;
					case "SectionType.Tv"         : Section = SectionType.Tv;         break;
					case "SectionType.Software"   : Section = SectionType.Software;   break;
					case "SectionType.Network"    : Section = SectionType.Network;    break;
					case "SectionType.Security"   : Section = SectionType.Security;   break;
					case "SectionType.Relaxation" : Section = SectionType.Relaxation; break;
					case "SectionType.Folders"    : Section = SectionType.Folders;    break;
					case "SectionType.Bookmarks"  : Section = SectionType.Bookmarks;  break;
					}

				this.Click ( this, new ObjectEventArgs<SectionType> ( Section ) );
				//---------------------------------------------------------------------------------------------------------------
				}
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Se produit lors d'un clic sur un élément de la page.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public event EventHandler Click;
		//***********************************************************************************************************************
		}
	//---------------------------------------------------------------------------------------------------------------------------
	#endregion
	//***************************************************************************************************************************

	} // Fin du namespace "Clubic.Panels"
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// FIN DU FICHIER
//*******************************************************************************************************************************
