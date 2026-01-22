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

// 3. Récupération des colonnes existantes dans grr_room
$columnsStmt = $pdo->query("SHOW COLUMNS FROM grr_room");
$columns = $columnsStmt->fetchAll(PDO::FETCH_COLUMN);

// Colonnes minimales obligatoires
$defaultValues = [
    "id" => null,
    "room_name" => "",
    "area_id" => 1,
    "comment_room" => "",
    "statut_room" => 0,
    "capacity" => 0,
    "order_display" => 0,
    "delais_max_resa_room" => 0,
    "delais_min_resa_room" => 0,
    "allow_action_on_conflict" => 0,
    "active" => 1
];

// On ne garde que les colonnes réellement présentes dans ta base
$insertColumns = array_intersect(array_keys($defaultValues), $columns);

// Construction dynamique de la requête INSERT
$colList = implode(", ", $insertColumns);
$placeholders = implode(", ", array_fill(0, count($insertColumns), "?"));

// 4. Récupération des ressources GRR existantes
$stmt = $pdo->query("SELECT id FROM grr_room ORDER BY id ASC");
$grrRooms = $stmt->fetchAll(PDO::FETCH_COLUMN);

// 5. Extraction des IDs API
$apiIds = array_column($equipements, "id_Equipement");

// 6. AJOUT des ressources manquantes
foreach ($equipements as $eq) {
    $id = $eq["id_Equipement"];
    $nom = $eq["nom_Equipement"];

    if (!in_array($id, $grrRooms)) {

        // Préparation des valeurs dans l'ordre des colonnes existantes
        $values = [];
        foreach ($insertColumns as $col) {
            if ($col === "id") $values[] = $id;
            elseif ($col === "room_name") $values[] = $nom;
            else $values[] = $defaultValues[$col];
        }

        $stmt = $pdo->prepare("INSERT INTO grr_room ($colList) VALUES ($placeholders)");
        $stmt->execute($values);
    }
}

// 7. MISE À JOUR des ressources existantes
foreach ($equipements as $eq) {
    $id = $eq["id_Equipement"];
    $nom = $eq["nom_Equipement"];

    $stmt = $pdo->prepare("UPDATE grr_room SET room_name = ? WHERE id = ?");
    $stmt->execute([$nom, $id]);
}

// 8. SUPPRESSION des ressources en trop
foreach ($grrRooms as $roomId) {
    if (!in_array($roomId, $apiIds)) {
        $stmt = $pdo->prepare("DELETE FROM grr_room WHERE id = ?");
        $stmt->execute([$roomId]);
    }
}

