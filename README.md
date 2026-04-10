# Whack-a-Mole-VR-

Jeu de réalité virtuelle pour Meta Quest 2, développé dans le cadre du cours Environnements Immersifs au Cégep de Victoriaville.

## Auteur

Nathan Reyes — Session Hiver 2026

## Description

Dans cette expérience VR, le joueur manipule un marteau pour frapper des cibles qui surgissent aléatoirement sur une table avant de disparaître.
Chaque cible touchée rapporte 10 points, et la partie se déroule sur une durée de 60 secondes.

## Fonctionnalités

- Interaction avec le marteau via le XR Interaction Toolkit (grab physique)
- Apparition aléatoire de cibles avec suppression automatique
- Structure de jeu complète : menu principal → gameplay → écran de fin → relance
- Interface utilisateur en espace 3D (score, minuterie, écrans)
- Retour haptique distinct lors de la prise du marteau et des impacts
- Effets sonores spatialisés lors de l’apparition des cibles

## Technologie

- Unity 6.3 (URP)
- XR Interaction Toolkit (version 3.x)
- OpenXR / Meta Quest Support
- TextMeshPro pour l'interface

## Lancer le projet

1. Cloner le repository
2. Ouvrir le projet avec Unity Hub (version 6.3 ou supérieure)
3. Installer les dépendances si nécessaire via le Package Manager
4. Charger la scène : Assets/Scenes/WhackAMole.unity
5. Test dans l’éditeur :
   - Ajouter le prefab XR Device Simulator dans la scène
6. Build pour Meta Quest :
   - Retirer le XR Device Simulator
   - Connecter le casque
   - Aller dans File → Build And Run
