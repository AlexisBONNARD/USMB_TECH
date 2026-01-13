from sentence_transformers import SentenceTransformer
from sklearn.metrics.pairwise import cosine_similarity
import numpy as np
import requests


# Chargement du meilleur modèle français d'association de synonymes
#  possibilité d'utiliser dangvantuan/sentence-camembert-large sinon
model = SentenceTransformer("Lajavaness/sentence-camembert-large")
Laboratoires = requests.get("https://api-usmbtech-hvevdvgbdwh7aqf5.francecentral-01.azurewebsites.net/api/Laboratoires").json()
#Equipements = requests.get("https://api-usmbtech-hvevdvgbdwh7aqf5.francecentral-01.azurewebsites.net/api/Equipements").json()
#PoleExpertise = requests.get("https://api-usmbtech-hvevdvgbdwh7aqf5.francecentral-01.azurewebsites.net/api/Pole_Expertises").json()
Prestation = requests.get("https://api-usmbtech-hvevdvgbdwh7aqf5.francecentral-01.azurewebsites.net/api/Prestations").json()
#PriseContacts= requests.get("https://api-usmbtech-hvevdvgbdwh7aqf5.francecentral-01.azurewebsites.net/api/Prise_Contacts").json()


# Liste des articles du site web
nomLabo = [ i["nom_Court"] for i in Laboratoires ]
descriptionLabo =  [ i["nom_Long"] for i in Laboratoires ]
'''
nomEquipements = [  i["nom_Equipement"] for i in Equipements]
descritpionTechEquipement =  [  i["description_Technique"] for i in Equipements]
poleExpertise = [  i["nom_Pole_Expertise"] for i in PoleExpertise]
poleExpertiseDesc = [  i["description_Pole_Expertise"] for i in PoleExpertise]
poleExpertiseNomContenu = [  i["nom_Contenu"] for i in PoleExpertise]
poleExpertiseDescContenu = [  i["description_Contenu"] for i in PoleExpertise]
'''
prestationNom = [  i["intitule_Prestation"] for i in Prestation]
prestationNomCourt = [  i["nom_Court"] for i in Prestation]
prestationDescription = [  i["description_Prestation"] for i in Prestation]
#nom_contact = [ i["nom_Contact"]+" "+i["prenom_Contact"] + " "+i["email_Contact"]+" "+i["description_besoins"] for i in PriseContacts]
articles = descriptionLabo+nomLabo+prestationNom+prestationNomCourt+prestationDescription#+nom_contact+nomEquipements+descritpionTechEquipement+poleExpertise+poleExpertiseDesc+poleExpertiseNomContenu+poleExpertiseDescContenu
# Pré-calcul des embeddings
article_embeddings = model.encode(articles)

# Recherche des articles
# args :
#   - query : string
#       contiens la recherche de l'utilisateur$
#   - top_k : int
#       permet d'obtenir un top
#       remarque : le premier résultat est souvent loin devant mais des fois plusieurs résultats sont proches
def search(query, top_k=5):
    query_emb = model.encode(query)
    scores = cosine_similarity([query_emb], article_embeddings)[0]
    results = [(scores[i], articles[i]) for i in range(len(articles))]
    results.sort(reverse=True)
    return results[:top_k]
