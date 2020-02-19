echo off
curl -v -b cookie -X PUT -F "space_template[zip]=@C:\Users\stvergen\Projects\altspace-scene-mre\UnityProject\template.zip" -F "space_template[game_engine_version]=20192" https://account.altvr.com/api/space_templates/1407374156806226825.json
