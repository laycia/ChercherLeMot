using System;
using System.Collections.Generic;
using System.IO;

namespace ChercherLeMot
{
    class Program
    {
        static void Main(string[] args)
        {
            
            List<string> dictionnaire = new List<string>();

            try
            {
                // Lecture de toutes les lignes du fichier et ajout dans la liste dictionnaire
                dictionnaire.AddRange(File.ReadAllLines("dictionnaire.txt"));
            }
            catch (IOException e)
            {
                Console.WriteLine("Une erreur est survenue lors de la lecture du fichier:");
                Console.WriteLine(e.Message);
                return; // Arrêter l'exécution si le fichier ne peut être lu
            }

            bool continuer = true;
            while (continuer)
            {
                Console.WriteLine("Entrez le/les mot(s) que vous souhaitez rechercher dans le dictionnaire (tapez 'end' pour terminer) :");
                var motsUtilisateur = new List<string>();
                string input;
                while ((input = Console.ReadLine().ToLower()) != "end")
                {
                    if (!string.IsNullOrWhiteSpace(input))
                    {
                        motsUtilisateur.Add(input);
                    }
                    else
                    {
                        Console.WriteLine("Veuillez entrer un mot valide ou 'end' pour terminer.");
                    }
                }

                foreach (var mot in motsUtilisateur)
                {
                    var correspondance = TrouverCorrespondance(mot, dictionnaire);

                    if (string.IsNullOrEmpty(correspondance))
                    {
                        Console.WriteLine($"{mot}: aucune correspondance trouvée.");
                    }
                    else
                    {
                        Console.WriteLine($"{mot} correspond à {correspondance}");
                    }
                }

                // Demander à l'utilisateur s'il souhaite continuer
                Console.WriteLine("Souhaitez-vous continuer à rechercher ? (oui/non)");
                var reponse = Console.ReadLine().ToLower();
                if (reponse != "oui")
                {
                    continuer = false;
                }
            }
        }

        static string TrouverCorrespondance(string motRecherche, List<string> dictionnaire)
        {
            foreach (var mot in dictionnaire)
            {
                if (EstAnagramme(motRecherche, mot))
                {
                    return mot;
                }
            }
            return null;
        }

        static bool EstAnagramme(string mot1, string mot2)
        {
            var arrMot1 = mot1.ToCharArray();
            var arrMot2 = mot2.ToCharArray();
            Array.Sort(arrMot1);
            Array.Sort(arrMot2);
            return new string(arrMot1) == new string(arrMot2);
        }
    }
}
