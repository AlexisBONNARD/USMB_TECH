<?php

// 1. Appel de l'API C#
$apiUrl = "https://api-usmbtech-hvevdvgbdwh7aqf5.francecentral-01.azurewebsites.net/api/Equipements";
$json = file_get_contents($apiUrl);
$equipements = json_decode($json, true);

// 2. Connexion MySQL GRR
$pdo = new PDO(
    "mysql:host=db4free.net;dbname=db_grr;charset=utf8",
    "grradmin",
    "dN8QKrYi!",
    [PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION]
);

// 3. Récupération des ressources GRR existantes
$stmt = $pdo->query("SELECT id FROM grr_room ORDER BY id ASC");
$grrRooms = $stmt->fetchAll(PDO::FETCH_COLUMN);

// 4. Extraction des IDs API
$apiIds = array_column($equipements, "id_Equipement");

// 5. AJOUT des ressources manquantes
foreach ($equipements as $eq) {
    $id = $eq["id_Equipement"];
    $nom = $eq["nom_Equipement"];

    if (!in_array($id, $grrRooms)) {

        // INSERT compatible GRR (toutes colonnes obligatoires)
        $stmt = $pdo->prepare("
            INSERT INTO grr_room (
                id,
                room_name,
                area_id,
                comment_room,
                statut_room,
                capacity,
                order_display,
                delais_max_resa_room,
                delais_min_resa_room,
                allow_action_on_conflict,
                active
            ) VALUES (
                ?, ?, 1, '', 0, 0, 0, 0, 0, 0, 1
            )
        ");
        $stmt->execute([$id, $nom]);
    }
}

// 6. MISE À JOUR des ressources existantes
foreach ($equipements as $eq) {
    $id = $eq["id_Equipement"];
    $nom = $eq["nom_Equipement"];

    $stmt = $pdo->prepare("UPDATE grr_room SET room_name = ? WHERE id = ?");
    $stmt->execute([$nom, $id]);
}

// 7. SUPPRESSION des ressources en trop
foreach ($grrRooms as $roomId) {
    if (!in_array($roomId, $apiIds)) {
        $stmt = $pdo->prepare("DELETE FROM grr_room WHERE id = ?");
        $stmt->execute([$roomId]);
    }
}

echo "✅ Synchronisation complète terminée.";
