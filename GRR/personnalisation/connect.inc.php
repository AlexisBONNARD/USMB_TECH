<?php
# Les quatre lignes suivantes sont � modifier selon votre configuration
# ligne suivante : le nom du serveur qui herberge votre base sql.
# Si c'est le m�me que celui qui heberge les scripts, mettre "localhost"
$dbHost = getenv('DB_HOST');
# ligne suivante : le nom de votre base sql
$dbDb = getenv('DB_NAME');
# ligne suivante : le nom de l'utilisateur sql qui a les droits sur la base
$dbUser = getenv('DB_USER');
# ligne suivante : le mot de passe de l'utilisateur sql ci-dessus
$dbPass = getenv('DB_PASSWORD');
# ligne suivante : Port MySQL laiss� par d�faut
$dbPort = getenv('DB_PORT');
?>
