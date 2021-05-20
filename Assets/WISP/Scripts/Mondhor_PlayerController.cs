using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //Les floats que le code utilise pour gérer la vitesse de déplacement du joueur (affichée en inspecteur pour des raisons de debug)
    [SerializeField]
    private float HorizontalSpeed = 0f;
    [SerializeField]
    private float VerticalSpeed = 0f;

    //La vitesse maximale que le joueur peut atteindre (changeable en inspecteur)
    [SerializeField]
    private float MaxSpeed = 0f;

    //Le facteur d'accélération du joueur (changeable en inspecteur)
    [SerializeField]
    private float Acceleration = 0f;

    //Le facteur de décélération du joueur (changeable en inspecteur)
    [SerializeField]
    private float Deceleration = 0f;

    //Le booléen indiquant si le déplacement est normalized ou non (oui j'ai fait un normalize à la main parce que le normalize d'unity ne marchait pas, ne me demandez pas pourquoi)
    private bool normalized = false;

    //Les booléens indiquant si on a appuyé sur les inputs horizontaux
    private bool D_Pressed;
    private bool Q_Pressed;

    //Pareil mais pour les inputs verticaux
    private bool Z_Pressed;
    private bool S_Pressed;


    //IMPORTANT
    //IMPORTANT
    //J'ai séparé dans le code le fontionnement des axes horizontaux et verticaux pour plus de clarté, donc quand j'explique le code, j'explique d'abord pour les axes horizontaux et j'indique dans un second temps que c'est la même chose mais pour les axes verticaux
    //IMPORTANT
    //IMPORTANT


    void Update()
    {
        #region DETECTION DES TOUCHES
        //Ici, on détecte simplement les imputs du joueur, et on les traduit par des booléens utilisables plus tard

        //Si on appuie sur D, on active un booléen
        if (Input.GetKey(KeyCode.D))
        {
            D_Pressed = true;
        }

        //Si on appuie sur Q, on active un booléen
        if (Input.GetKey(KeyCode.Q))
        {
            Q_Pressed = true;
        }

        //Si on relâche D, on désactive un booléen
        if (Input.GetKeyUp(KeyCode.D))
        {
            D_Pressed = false;
        }

        //Si on relâche Q, on désactive un booléen
        if (Input.GetKeyUp(KeyCode.Q))
        {
            Q_Pressed = false;
        }



        //Si on appuie sur Z, on active un booléen
        if (Input.GetKey(KeyCode.Z))
        {
            Z_Pressed = true;
        }

        //Si on appuie sur S, on active un booléen
        if (Input.GetKey(KeyCode.S))
        {
            S_Pressed = true;
        }

        //Si on relâche Z, on désactive un booléen
        if (Input.GetKeyUp(KeyCode.Z))
        {
            Z_Pressed = false;
        }

        //Si on relâche S, on désactive un booléen
        if (Input.GetKeyUp(KeyCode.S))
        {
            S_Pressed = false;
        }
        #endregion DETECTION DES TOUCHES

        #region TOUCHES PRESSEES
        //Ici, on gère ce que le code va faire quand on appuie sur des touches ; de façon générale, on a ici les déplacements et le drag


        //Si la touche D est maintenue, on se déplace.
        if (D_Pressed == true)
        {
            //Si on n'a pas atteint la vitesse maximale, on accélère pendant notre déplacement
            if(HorizontalSpeed < MaxSpeed)
            {
                transform.position += new Vector3(HorizontalSpeed, 0, 0);
                HorizontalSpeed += Acceleration * Time.deltaTime;
            }
            //Quand on a atteint la vitesse maximale, on se contente de se déplacer
            else
            {
                transform.position += new Vector3(HorizontalSpeed, 0, 0);
            }
        }

        //Pareil qu'au-dessus, mais on inverse les signes des nombres qu'on utilise pour se déplacer dans l'autre sens
        if (Q_Pressed == true)
        {
            //Ici, on soustrait le facteur d'accélération au lieu de l'additionner pour aller dans l'autre sens
            if(HorizontalSpeed > (MaxSpeed * -1))
            {
                transform.position += new Vector3(HorizontalSpeed, 0, 0);
                HorizontalSpeed -= Acceleration * Time.deltaTime;
            }
            else
            {
                transform.position += new Vector3(HorizontalSpeed, 0, 0);
            }

        }


        //Pareil qu'au-dessus, mais pour l'axe vertical
        
        if (Z_Pressed == true)
        {
            if (VerticalSpeed < MaxSpeed)
            {
                transform.position += new Vector3(0, VerticalSpeed, 0);
                VerticalSpeed += Acceleration * Time.deltaTime;
            }
            else
            {
                transform.position += new Vector3(0, VerticalSpeed, 0);
            }
        }

        if (S_Pressed == true)
        {
            if (VerticalSpeed > (MaxSpeed * -1))
            {
                transform.position += new Vector3(0, VerticalSpeed, 0);
                VerticalSpeed -= Acceleration * Time.deltaTime;
            }
            else
            {
                transform.position += new Vector3(0, VerticalSpeed, 0);
            }

        }



        //Ici, on normalise les vecteurs de déplacements pour ne pas aller plus vite en diagonale ; la méthode normalize ne marchait pas, et ça allait plus vite de faire ça plutôt que de faire fonctionner la méthode, donc je l'ai fait à la main
        //On va vérifier qu'au moins une diagonale est utilisée ET que la MaxSpeed n'est pas normalisée, puis on divise MaxSpeed par racine de 2 et on indique au code que la vitesse est bien normalisée pour ne pas qu'il passe plusieurs fois dans la boucle
        
        //Diagonale Z+D
        if (Z_Pressed && D_Pressed && normalized == false)
        {
            MaxSpeed = MaxSpeed / 1.41f;
            normalized = true;
        }
        //Diagonale Z+Q
        else if (Z_Pressed && Q_Pressed && normalized == false)
        {
            MaxSpeed = MaxSpeed / 1.41f;
            normalized = true;
        }
        //Diagonale S+D
        else if (S_Pressed && D_Pressed && normalized == false)
        {
            MaxSpeed = MaxSpeed / 1.41f;
            normalized = true;
        }
        //Diagonale S+Q
        else if (S_Pressed && Q_Pressed && normalized == false)
        {
            MaxSpeed = MaxSpeed / 1.41f;
            normalized = true;
        }

        //Le script ne passera ici que si aucune diagonale n'est utilisée (puisqu'il les vérifie toutes une par une avant d'arriver ici), et si la vitesse est normalisée alors on la rétablit à sa valeur d'origine
        else if (normalized == true)
        {
            MaxSpeed = MaxSpeed * 1.41f;
            normalized = false;
        }



        //Ici, on revérifie par sécurité si les vitesses ne sont pas au-dessus de la MaxSpeed pour les réajuster, c.f lignes 101 et 117
        if (HorizontalSpeed >= MaxSpeed)
        {
            HorizontalSpeed = MaxSpeed;
        }

        if (HorizontalSpeed <= MaxSpeed * -1)
        {
            HorizontalSpeed = MaxSpeed * -1;
        }

        if (VerticalSpeed >= MaxSpeed)
        {
            VerticalSpeed = MaxSpeed;
        }

        if (VerticalSpeed <= MaxSpeed * -1)
        {
            VerticalSpeed = MaxSpeed * -1;
        }

        #endregion TOUCHES PRESSEES

        #region TOUCHES RELACHEES
        //Ici, on gère ce que le code va faire lorsqu'on n'appuie pas sur les touches ; de façon générale, on a ici les drags de ralentissements et les bugfix de tremblements


        //Si aucune touche n'est pressée ET qu'on bouge vers la droite, alors on ralentit
        if (D_Pressed == false && Q_Pressed == false && HorizontalSpeed > 0)
        {
            transform.position += new Vector3(HorizontalSpeed, 0, 0);
            HorizontalSpeed -= Deceleration * Time.deltaTime;
        }

        //Si aucune touche n'est pressée ET qu'on bouge vers la gauche, alors on ralentit
        if (D_Pressed == false && Q_Pressed == false && HorizontalSpeed < 0)
        {
            transform.position += new Vector3(HorizontalSpeed, 0, 0);
            HorizontalSpeed += Deceleration * Time.deltaTime;
        }

        //Si aucune touche n'est pressée ET qu'on est très très lent, alors on s'arrête (bugfix tremblement)
        if (D_Pressed == false && Q_Pressed == false && HorizontalSpeed >= -0.01 && HorizontalSpeed <= 0.01)
        {
            HorizontalSpeed = 0;
        }


        //Pareil mais pour l'axe vertical
        if (Z_Pressed == false && S_Pressed == false && VerticalSpeed > 0)
        {
            transform.position += new Vector3(0, VerticalSpeed, 0);
            VerticalSpeed -= Deceleration * Time.deltaTime;
        }

        if (Z_Pressed == false && S_Pressed == false && VerticalSpeed < 0)
        {
            transform.position += new Vector3(0, VerticalSpeed, 0);
            VerticalSpeed += Deceleration * Time.deltaTime;
        }

        if (Z_Pressed == false && S_Pressed == false && VerticalSpeed >= -0.01 && VerticalSpeed <= 0.01)
        {
            VerticalSpeed = 0;
        }

        #endregion TOUCHES RELACHEES
    }
}
