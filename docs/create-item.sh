curl -X 'POST' \
    'http://localhost:5125/catalogue/88BD0000-F588-04D9-3C54-08DC4E40D836/items/41520e0e-7649-4249-8d18-c0a02421a5e6' \
    -H 'accept: application/json' \
    -H 'X-Api-Key: _ic_9fad2b4be649887c70a58b869c8838075b0dcf91554e64e2b367ba3079d079f5_fea' \
    -H 'Content-Type: application/json' \
    -d '{
  "name": "Cool New Widget G",
  "description": "Cool New Created",
  "primaryCategoryId": "88BD0000-F588-04D9-F9CD-08DC4E40D83E",
  "categoryIds": [
    "88BD0000-F588-04D9-F9CD-08DC4E40D83E"
  ]
}'
