﻿//*******************************************************************************************************************************
// DEBUT DU FICHIER
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// Nom           : Offset.cs
// Auteur        : Nicolas Dagnas
// Description   : Implémentation de l'objet Offset
// Créé le       : 17/01/2015
// Modifié le    : 17/01/2015
//*******************************************************************************************************************************

//-------------------------------------------------------------------------------------------------------------------------------
#region Using directives
//-------------------------------------------------------------------------------------------------------------------------------
using System;
using System.Windows.Media;
//-------------------------------------------------------------------------------------------------------------------------------
#endregion
//-------------------------------------------------------------------------------------------------------------------------------

//*******************************************************************************************************************************
// Début du bloc "System.Windows"
//*******************************************************************************************************************************
namespace System.Windows
	{

	//   ###   #####  #####   ####  #####  #####
	//  #   #  #      #      #      #        #  
	//  #   #  ###    ###     ###   ###      #  
	//  #   #  #      #          #  #        #  
	//   ###   #      #      ####   #####    #  

	//***************************************************************************************************************************
	// Structure Offset
	//***************************************************************************************************************************
	#region // Déclaration et Implémentation de l'Objet
	//---------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Fournit un objet Offset.
	/// </summary>
	//---------------------------------------------------------------------------------------------------------------------------
	public struct Offset
		{
		//***********************************************************************************************************************
		/// <summary>
		/// Obtiens ou définit la valeur courante du décallage.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public double Value { get; set; }
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Obtiens ou définit le translateur utilisé pour le décallage.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public TranslateTransform Transform { get; set; }
		//***********************************************************************************************************************
		}
	//---------------------------------------------------------------------------------------------------------------------------
	#endregion
	//***************************************************************************************************************************

	} // Fin du namespace "System.Windows"
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// FIN DU FICHIER
//*******************************************************************************************************************************
