﻿//*******************************************************************************************************************************
// DEBUT DU FICHIER
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// Nom           : XElementUtils.cs
// Auteur        : Nicolas Dagnas
// Description   : Implémentation de l'objet XElementUtils
// Créé le       : 29/06/2013
// Modifié le    : 29/06/2013
//*******************************************************************************************************************************

//-------------------------------------------------------------------------------------------------------------------------------
#region Using directives
//-------------------------------------------------------------------------------------------------------------------------------
using System;
using System.Xml.Linq;
//-------------------------------------------------------------------------------------------------------------------------------
#endregion
//-------------------------------------------------------------------------------------------------------------------------------

//*******************************************************************************************************************************
// Début du bloc "System"
//*******************************************************************************************************************************
namespace System
	{

	//  #   #  #####  #      #####  #   #  #####  #   #  #####
	//   # #   #      #      #      ## ##  #      ##  #    #  
	//    #    ###    #      ###    # # #  ###    # # #    #  
	//   # #   #      #      #      #   #  #      #  ##    #  
	//  #   #  #####  #####  #####  #   #  #####  #   #    #  

	//***************************************************************************************************************************
	// Classe XElementUtils
	//***************************************************************************************************************************
	#region // Déclaration et Implémentation de l'Objet
	//---------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Fournit des méthodes utilisées pour manipuler les éléments XElement.
	/// </summary>
	//---------------------------------------------------------------------------------------------------------------------------
	internal static class XElementUtils
		{
		//***********************************************************************************************************************
		/// <summary>
		/// Obtiens la chaine.
		/// </summary>
		/// <param name="Self">Objet <b>XElement</b> contenant la valeur.</param>
		/// <param name="AttributeName">Nom de la valeur.</param>
		/// <returns>Contenue chaine de la valeur.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public static string GetAttribute ( this XElement Self, XName AttributeName )
			{
			//-------------------------------------------------------------------------------------------------------------------
			if ( Self != null && AttributeName != null )
				{
				//---------------------------------------------------------------------------------------------------------------
				XAttribute XValue = Self.Attribute ( AttributeName );

				if ( XValue != null && XValue.Value is string ) return XValue.Value;
				//---------------------------------------------------------------------------------------------------------------
				}
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			return String.Empty;
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Obtiens la chaine.
		/// </summary>
		/// <param name="Self">Objet <b>XElement</b> contenant la valeur.</param>
		/// <param name="AttributeName">Nom de la valeur.</param>
		/// <returns>Contenue chaine de la valeur.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public static string GetString ( this XElement Self, XName AttributeName )
			{
			//-------------------------------------------------------------------------------------------------------------------
			if ( Self != null && AttributeName != null )
				{
				//---------------------------------------------------------------------------------------------------------------
				XElement XValue = Self.Element ( AttributeName );

				if ( XValue != null && XValue.Value is string ) return (string)XValue.Value;
				//---------------------------------------------------------------------------------------------------------------
				}
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			return String.Empty;
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		}
	//---------------------------------------------------------------------------------------------------------------------------
	#endregion
	//***************************************************************************************************************************

	} // Fin du namespace "System"
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// FIN DU FICHIER
//*******************************************************************************************************************************
