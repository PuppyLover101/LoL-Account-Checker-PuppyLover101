import os, urllib, urllib2, json

champs_vers = '6.6.1'
images_vers = '6.6.1'
runes_vers = '6.6.1'

base_url = 'http://ddragon.leagueoflegends.com/cdn/'
champs_url = '%s%s/data/en_US/championFull.json' % (base_url, champs_vers)
img_url = '%s%s/img/champion/' % (base_url, images_vers)
runes_url = '%s%s/data/en_US/rune.json' %(base_url, runes_vers)

def loadJsonFromUrl(url):
	r = urllib2.urlopen(url)
	str_r = r.read().decode('utf-8')
	return json.loads(str_r)

obj = loadJsonFromUrl(champs_url)

output = []

for ck, champion in sorted(obj['data'].items()):
    skins = []

    for skin in champion['skins']:
        skins.append({'Id': int(skin['id']), 'Name': skin['name'], 'Num': skin['num']})

    output.append({'Id': int(champion['key']), 'StrId': champion['id'], 'Name': champion['name'], 'Skins': skins})

    print(champion['name'])


with open('Champions.json', 'w') as f:
	json.dump(output, f)

with open('Champions.Version', 'w') as f:
	f.write(champs_vers)

#if not os.path.exists('images'):
#	os.makedirs('images')

'''for champion in output:
	filename = champion['StrId'] + '.png'
	
	url = img_url + filename
	
	filepath = os.path.join('images', filename)
	
	urllib.urlretrieve(url, filepath)
	
	print 'Downloading image for champion ' + champion['Name']'''

#with open('Images.Version', 'w') as f:
#	f.write(images_vers)

def getRuneType(str_type):
	type = {
		'red':		1,
		'yellow':	2,
		'blue':		3,
		'black':	4
	}
	return type[str_type] if str_type in type else 0


obj = loadJsonFromUrl(runes_url)

output = []

for rune_id in obj['data']:
	rune = {
		'Id':			int(rune_id),
		'Name':			obj['data'][rune_id]['name'],
		'Description':	obj['data'][rune_id]['description'],
		'Tier': 		int(obj['data'][rune_id]['rune']['tier']),
		'Type':			getRuneType(obj['data'][rune_id]['rune']['type'])
	}
	output.append(rune)
	
	print rune['Name']

with open('Runes.json', 'w') as f:
	json.dump(output, f)

with open('Runes.Version', 'w') as f:
	f.write(runes_vers)

print "Press Enter to continue..." 
raw_input()

'Made by PuppyLover101/CYCOABHI :)
