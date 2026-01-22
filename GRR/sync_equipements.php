<?php

// 1. Appel de l'API C#
$apiUrl = "https://api-usmbtech-hvevdvgbdwh7aqf5.francecentral-01.azurewebsites.net/api/Equipements";
$json = file_get_contents($apiUrl);
$equipements = json_decode($json, true);

// 2. Connexion à la base GRR
$pdo = new PDO(
    "mysql:host=db4free.net;dbname=db_grr;charset=utf8",
    "grradmin",
    "dN8QKrYi!",
    [PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION]
);

// 3. Mise à jour des noms de ressources
$updated = 0;
foreach ($equipements as $eq) {
    if (!isset($eq["id_Equipement"]) || !isset($eq["nom_Equipement"])) continue;

    $id = $eq["id_Equipement"];
    $nom = $eq["nom_Equipement"];

    $stmt = $pdo->prepare("UPDATE grr_room SET room_name = ? WHERE id = ?");
    $stmt->execute([$nom, $id]);

    if ($stmt->rowCount() > 0) {
        $updated++;
    }
}

echo "✅ Synchronisation terminée : $updated ressources mises à jour.";
