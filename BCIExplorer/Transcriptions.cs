using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace BCIExplorer
{
	public class Transcriptions
	{
		private List<Event> eventSerializationWrapper;

		public Transcriptions()
		{
		}

		public Transcriptions( int track )
		{
			eventSerializationWrapper = new List<Event>();
			Events = new SortedDictionary<double, string>();
			if( track == 1 )
			{
				Events.Add( 3845120, "Cut to video" );
				Events.Add( 3845121, "Music starts" );
				Events.Add( 5376000, "Singer starts singing" );
				Events.Add( 6446080, "Dog looking sad, over man's shoulder" );
				Events.Add( 7055360, "Subtle rain & thunder" );
				Events.Add( 7055361, "Car door closed" );
				Events.Add( 8192000, "Horse whinnying (vrinsker)" );
				Events.Add( 8453120, "Puppy barks once" );
				Events.Add( 8949760, "Music intensifies / Horse jumps out of its pen" );
				Events.Add( 9216000, "Horse whinnying (vrinsker)" );
				Events.Add( 9574400, "Man brakes his car" );
				Events.Add( 9687040, "Horses surrounds car" );
				Events.Add( 9984000, "Horse whinnying (vrinsker)" );
				Events.Add( 10158080, "Close-up of puppy returning home" );
				Events.Add( 10624000, "Puppy barks happily" );
				Events.Add( 10880000, "Horse whinnying (vrinsker)" );
				Events.Add( 11212800, "Cut to red screen (logo)" );
				Events.Add( 11525120, "Cut to black / music stops" );
				//Movie 11- puppy ends
				Events.Add( 12805120, "Cut to video & Music starts" );
				Events.Add( 13440000, "Singers start singing" );
				Events.Add( 13813760, "Schoolgirl takes off her shorts" );
				Events.Add( 14080000, "Grab your friends and head out to the sun" );
				Events.Add( 15360000, "Little bell rings once" );
				Events.Add( 17443840, "2 guys toasts, with girls in background" );
				Events.Add( 17935360, "Girl 1 tumbles over guy 1, so they both lie down" );
				Events.Add( 18247680, "Girl 1 sits on top of guy 1, both looking intensively at eachother" );
				Events.Add( 18406400, "Girl 1 and guy 1 kiss eachother" );
				Events.Add( 18708480, "Mermaid-reference! (Girl 2 flings her wet hair, in the sunset(?))" );
				Events.Add( 19179520, "Girl 1 gets up from the sand" );
				Events.Add( 19333120, "Guy looks at the girl" );
				Events.Add( 19496960, "Girl 1 runs away, smiling back at guy 1" );
				Events.Add( 19681280, "Guy 1 starts to follow girl 1" );
				Events.Add( 19962880, "Girl 1 gets blown up + explosion sound" );
				Events.Add( 20029440, "Blood on the screen + splat sound" );
				Events.Add( 20096000, "Girl 2 screams" );
				Events.Add( 20259840, "Guy 1 yells Nooo!" );
				Events.Add( 20413440, "3 x beeping sound, then guy 1 explodes + (blood)explosion sound" );
				Events.Add( 20546560, "Guy 2, holding in hands with girl 2, yells Let's get outta here" );
				Events.Add( 20736000, "Girl 2 breaths heavy, while they are trying to escape" );
				Events.Add( 21186560, "Guy 2 yells, at girl 2, You are slowing me down" );
				Events.Add( 21288960, "Girl 2 screams at guy 2, Shaun(possibly?)" );
				Events.Add( 21376000, "Girl 2 shrieks loudly, while guy 2 runs away" );
				Events.Add( 20169600, "Clicking noise, then guy 2 explodes" );
				Events.Add( 21560320, "Girl 2 shrieks even louder, while blood splats all over her" );
				Events.Add( 21729280, "Girl 2's shrieking, stops" );
				Events.Add( 22056960, "Girl 2 looks at guy 2's torn flesh/blood on her body" );
				Events.Add( 22154240, "Clip of guy 2's dismembered arm + ringing deafness sound starts" );
				Events.Add( 22568960, "Girl 2 drops down on her knees + thumping noise" );
				Events.Add( 23091200, "Girl 2 whimps and shivers" );
				Events.Add( 23464960, "Girl 2 starts screaming" );
				Events.Add( 23997440, "Cut to fence with signpost Restricted area + distant screaming" );
				Events.Add( 24161280, "Explosion in the background + mushroom cloud / Screaming stops / Limbs are flung in the air" );
				Events.Add( 24970240, "Text fades in This is what happens" );
				Events.Add( 25169920, "Text fades in when you slack off" );
				Events.Add( 25763840, "Cut to logo (dark screen) Stay in school + subtle sound of waves" );
				Events.Add( 26245120, "Cut to black" );
				// Movie 2 Beach ends
				Events.Add( 27525120, "Father sees the business car, in the intersection + indicator sound, then looks to the left" );
				Events.Add( 27729920, "Cut to business car perspective / Father starts to pull out on the road" );
				Events.Add( 28016640, "Business man notices the father pulls out in front of him + gasping sound" );
				Events.Add( 28303360, "Panview of business man about to collide with father + braking sound / Everything slows down to a halt" );
				Events.Add( 28380160, "Subtle humming background noise" );
				Events.Add( 28769280, "Business man exhales heavily" );
				Events.Add( 29056000, "Father breaths heavily" );
				Events.Add( 29322240, "Car door opens" );
				Events.Add( 29475840, "Business man places foot on the road + footstep sound" );
				Events.Add( 29475841, "Both walks towards eachother, father says Mate..." );
				Events.Add( 30187520, "Business man says You just pulled out..." );
				Events.Add( 30556160, "Father says well, come on mate" );
				Events.Add( 31201280, "Business man says I know, if I had gone a little slower..." );
				Events.Add( 31457280, "Whoosing background noise + business car moves a little forward + tire noise" );
				Events.Add( 31580160, "Business car stops, background noise stops" );
				Events.Add( 31892480, "Father says I've got a boy in the back" );
				Events.Add( 32348160, "Cut to boy in the backseat of father's car" );
				Events.Add( 32696320, "Business man says I came too fast" );
				Events.Add( 33254400, "Business man says I'm sorry" );
				Events.Add( 33382400, "Father exhales heavily, putting his hands on his neck" );
				Events.Add( 33617920, "Cut overview: business man re-enters his car" );
				Events.Add( 33884160, "Seatbelt click" );
				Events.Add( 33950720, "Background heartbeat sound intensifies" );
				Events.Add( 34181120, "Father looks at his kid, exhaling + semi-whimpering" );
				Events.Add( 34344960, "Cut to the kid, looking at the father" );
				Events.Add( 34503680, "Seatbelt click + Time resumes" );
				Events.Add( 34590720, "Car crash into the side of father + loud crash" );
				Events.Add( 34708480, "Fade to black + sound fades out" );
				Events.Add( 34851840, "Fade to text Other people makes mistakes" );
				Events.Add( 35205120, "Cut to black" );
				//Movie 3 - carcrash ends   
				Events.Add( 36485120, "Cut to warning" );
				Events.Add( 36879360, "Cut to video + engine sound" );
				Events.Add( 37186560, "A person says something uneligble" );
				Events.Add( 37304320, "A few bumping noises" );
				Events.Add( 38174720, "The car stops, and a girl in white stands in the middle of the road" );
				Events.Add( 38671360, "Girl stands in front of camera / JUMPSCARE 1 / Passengers starts to scream" );
				Events.Add( 38906880, "Car starts to reverse / reversing sound + motor gassing up" );
				Events.Add( 39802880, "Cut to dark screen with japanese symbols + sounds fades out" );
				Events.Add( 40058880, "Girl stands with a laptop, with some uneligble text + rumbling background sound fades in" );
				Events.Add( 40898560, "Fading to white + rumbling background sound fades out" );
				Events.Add( 41006080, "Fade to driving car + a sharp, loud sound" );
				Events.Add( 41364480, "The car stops, and a girl in white stands in the middle of the road" );
				Events.Add( 41871360, "Girl stands in front of camera / JUMPSCARE 2 / Passengers starts to scream" );
				Events.Add( 42106880, "Car starts to reverse / reversing sound + motor gassing up" );
				Events.Add( 42613760, "Fade to white" );
				Events.Add( 42680320, "Fade to slowmotion driving car + slowed-down sounds (low frequency rumbling)" );
				Events.Add( 43827200, "The car stops, and a girl in white stands in the middle of the road" );
				Events.Add( 45629440, "Girl stands in front of camera / SLOW JUMPSCARE 3 / Passengers starts to scream (slow)" );
				Events.Add( 46494720, "Cut to to logo + sharp clang sound" );
				Events.Add( 47232000, "Cut to black" );
				// Movie 4 - Japanese ends
				Events.Add( 48517120, "Cut to video" );
				Events.Add( 48640000, "Sound of a river + speaker starts speaking" );
				Events.Add( 49361920, "Man in orange overalls comes running in, while screaming" );
				Events.Add( 49771520, "Man jumps on the bear, that starts to roar" );
				Events.Add( 49920000, "Man falls to the ground" );
				Events.Add( 50247680, "Man and bear starts to box" );
				Events.Add( 50437120, "Man hits bear in stomach + punch sound" );
				Events.Add( 50688000, "Bears roars" );
				Events.Add( 50816000, "Bears starts to shuffle + man makes kungfu sounds" );
				Events.Add( 50974720, "Bear growls, and kicks man in stomach" );
				Events.Add( 51179520, "Bear kicks man on his leg" );
				Events.Add( 51338240, "Man says Oh look, an eagle, while pointing into the air" );
				Events.Add( 51507200, "Bear looks up in the air" );
				Events.Add( 51599360, "Man kicks bear in the balls + kick to the nuts sound" );
				Events.Add( 51676160, "Bear cries out in pain, while man picks up the salmon" );
				Events.Add( 51911680, "Cut to logo + speaker starts speaking again" );
				Events.Add( 52387840, "Cut to black" );
				//Movie 5 - Bear fight ends
				Events.Add( 53667840, "Cut to video / Guy starts to talk" );
				Events.Add( 54144000, "Guy sighs" );
				Events.Add( 54497280, "South Park" );
				Events.Add( 54901760, "Does not have souls / Nods his head and adding pressure on souls" );
				Events.Add( 55562240, "Looks away, then back / We do have souls" );
				Events.Add( 55925760, "Looks away / Lately I've been called a fat ginger / Looks back" );
				Events.Add( 56770560, "Sighs, then whipes his nose" );
				Events.Add( 56960000, "Sniffs" );
				Events.Add( 57323520, "Raises his forehead" );
				Events.Add( 57799680, "Looks away, raises his forehead, then sighs" );
				Events.Add( 58081280, "Yells Gingers have souls!" );
				Events.Add( 58275840, "Cuts to black" );
				//Movie 6 - Redhead ends
				Events.Add( 59555840, "Fade to video + guy in grey shirt (guy 1) starts to speak: Kaere 15-aarige mig + piano starts playing" );
				Events.Add( 59904000, "Cut to woman with long dark hair (woman 1), who says: Kaere mig.." );
				Events.Add( 60037120, "Cut to young woman with blond hair and black top (woman 2), who says: paa 15" );
				Events.Add( 60154880, "Cut to woman with long red curly hair (woman 3), who says: 15" );
				Events.Add( 60231680, "Cut to bald man (guy 2), who says: Kaere mig, 15 aar" );
				Events.Add( 60390400, "Fade to black" );
				Events.Add( 60441600, "Fade to young boy with brown hair, who is smiling + woman speakover 3: Du skal ikke tro paa at skulderpuder..." );
				Events.Add( 60579840, "Cut to woman 3" );
				Events.Add( 60723200, "Cut to close up of woman with a pearl earing (woman 4), saying: Ik' smart" );
				Events.Add( 60840960, "Cut to woung woman with brown hair (woman 5), saying: Tag nu de piercinger af.." );
				Events.Add( 61004800, "Cut to woman 3, who shakes head, while saying: Det holder bare ikke.." );
				Events.Add( 61153280, "Cut to woman 4, who says: Lad nu vaer" );
				Events.Add( 61245440, "Cut to guy 2, who says Lad nu vaer" );
				Events.Add( 61312000, "Cut to guy 1, who says Lad nu vaer" );
				Events.Add( 61404160, "Cut to woman 1, who says: Med at goere noget som JEG ikke ville ha' gjort" );
				Events.Add( 61639680, "Cut to woman 5, who smiles with closed eyes, while tilting her head to the left + woman 4 starts to speak: Lad nu vaer.." );
				Events.Add( 61752320, "Cut to woman 4, who says: Med at sove i timerne.., while blinking and tilting her head a tiny bit" );
				Events.Add( 61972480, "Cut to guy 2, who says: Det koster.." );
				Events.Add( 62120960, "Guy 2 blinks once, then smiles" );
				Events.Add( 62336000, "Woman 2 speakover: Lad nu vaer' med at tud'" );
				Events.Add( 62458880, "Cut to woman 2: over ham fra niende (9.).." );
				Events.Add( 62612480, "Woman 2 blinks once" );
				Events.Add( 62766080, "Woman 2 shakes her head, and her voice lowers a little in pitch: saa har han ikke fortjent dig.." );
				Events.Add( 62848000, "Fade to black / cut to close up of a scar on a turning arm, while light dims up" );
				Events.Add( 63104000, "Woman 3 speakover: Det er det her, du skal leve med.." );
				// Movie 7 - Skin cancer ends

			}
			else if( track == 3 )
			{
				Events.Add( 3845120, "Cut to warning" );
				Events.Add( 4224000, "Cut to video + engine sound" );
				Events.Add( 4480000, "A person says something uneligble" );
				Events.Add( 4608000, "A few bumping noises" );
				Events.Add( 5376000, "The car stops, and a girl in white stands in the middle of the road" );
				Events.Add( 6031360, "Girl stands in front of camera / JUMPSCARE 1 / Passengers starts to scream" );
				Events.Add( 6272000, "Car starts to reverse / reversing sound + motor gassing up" );
				Events.Add( 7162880, "Cut to dark screen with japanese symbols + sounds fades out" );
				Events.Add( 7418880, "Girl stands with a laptop, with some uneligble text + rumbling background sound fades in" );
				Events.Add( 8192000, "Fading to white + rumbling background sound fades out" );
				Events.Add( 8320000, "Fade to driving car + a sharp, loud sound" );
				Events.Add( 8832000, "The car stops, and a girl in white stands in the middle of the road" );
				Events.Add( 9231360, "Girl stands in front of camera / JUMPSCARE 2 / Passengers starts to scream" );
				Events.Add( 9600000, "Car starts to reverse / reversing sound + motor gassing up" );
				Events.Add( 9984000, "Fade to white" );
				Events.Add( 10240000, "Fade to slowmotion driving car + slowed-down sounds (low frequency rumbling)" );
				Events.Add( 11136000, "The car stops, and a girl in white stands in the middle of the road" );
				Events.Add( 12984320, "Girl stands in front of camera / SLOW JUMPSCARE 3 / Passengers starts to scream (slow)" );
				Events.Add( 13854720, "Cut to to logo + sharp clang sound" );
				Events.Add( 14592000, "Cut to black" );
				// Movie 1 japanese ends
				Events.Add( 15877120, "Cut to video" );
				Events.Add( 16128000, "Father sees the business car, in the intersection + indicator sound, then looks to the left" );
				Events.Add( 16368640, "Cut to business car perspective / Father starts to pull out on the road" );
				Events.Add( 16512000, "Business man notices the father pulls out in front of him + gasping sound" );
				Events.Add( 16640000, "Panview of business man about to collide with father + braking sound / Everything slows down to a halt" );
				Events.Add( 16768000, "Subtle humming background noise" );
				Events.Add( 17152000, "Business man exhales heavily" );
				Events.Add( 17408000, "Father breaths heavily" );
				Events.Add( 17674240, "Car door opens" );
				Events.Add( 17792000, "Business man places foot on the road + footstep sound" );
				Events.Add( 18048000, "Both walks towards eachother, father says Mate..." );
				Events.Add( 18560000, "Business man says You just pulled out..." );
				Events.Add( 18816000, "Father says well, come on mate" );
				Events.Add( 19456000, "Business man says I know, if I had gone a little slower.." );
				Events.Add( 19809280, "Whoosing background noise + business car moves a little forward + tire noise" );
				Events.Add( 19932160, "Business car stops, background noise stops" );
				Events.Add( 20224000, "Father says I've got a boy in the back" );
				Events.Add( 20700160, "Cut to boy in the backseat of father's car" );
				Events.Add( 20992000, "Business man says I came too fast" );
				Events.Add( 21504000, "Business man says I'm sorry" );
				Events.Add( 21632000, "Father exhales heavily, putting his hands on his neck" );
				Events.Add( 21888000, "Cut overview: business man re-enters his car" );
				Events.Add( 22144000, "Seatbelt click" );
				Events.Add( 22272000, "Background heartbeat sound intensifies" );
				Events.Add( 22528000, "Father looks at his kid, exhaling + semi-whimpering" );
				Events.Add( 22696960, "Cut to the kid, looking at the father" );
				Events.Add( 22784000, "Seatbelt click + Time resumes" );
				Events.Add( 22942720, "Car crash into the side of father + loud crash" );
				Events.Add( 23060480, "Fade to black + sound fades out" );
				Events.Add( 23183360, "Fade to text Other people makes mistakes" );
				Events.Add( 23557120, "Cut to black" );
				//Movie 2 car crash ends
				Events.Add( 24837120, "Cut to video & Music starts" );
				Events.Add( 24837121, "Singers start singing" );
				Events.Add( 25472000, "Schoolgirl takes off her shorts" );
				Events.Add( 26112000, "Grab your friends and head out to the sun" );
				Events.Add( 27392000, "Little bell rings once" );
				Events.Add( 29532160, "2 guys toasts, with girls in background" );
				Events.Add( 29967360, "Girl 1 tumbles over guy 1, so they both lie down" );
				Events.Add( 30279680, "Girl 1 sits on top of guy 1, both looking intensively at eachother" );
				Events.Add( 30551040, "Girl 1 and guy 1 kiss eachother" );
				Events.Add( 30740480, "Mermaid-reference! (Girl 2 flings her wet hair, in the sunset(?))" );
				Events.Add( 31211520, "Girl 1 gets up from the sand" );
				Events.Add( 31390720, "Guy looks at the girl" );
				Events.Add( 31528960, "Girl 1 runs away, smiling back at guy 1" );
				Events.Add( 31713280, "Guy 1 starts to follow girl 1" );
				Events.Add( 31994880, "Girl 1 gets blown up + explosion sound" );
				Events.Add( 32128000, "Blood on the screen + splat sound" );
				Events.Add( 32230400, "Girl 2 screams" );
				Events.Add( 32256000, "Guy 1 yells Nooo!" );
				Events.Add( 32384000, "3 x beeping sound, then guy 1 explodes + (blood)explosion sound" );
				Events.Add( 32512000, "Guy 2, holding in hands with girl 2, yells Let's get outta here" );
				Events.Add( 32768000, "Girl 2 breaths heavy, while they are trying to escape" );
				Events.Add( 33152000, "Guy 2 yells, at girl 2, You are slowing me down" );
				Events.Add( 33280000, "Girl 2 screams at guy 2, Shaun(possibly?)" );
				Events.Add( 33408000, "Girl 2 shrieks loudly, while guy 2 runs away" );
				Events.Add( 33546240, "Clicking noise, then guy 2 explodes" );
				Events.Add( 33638400, "Girl 2 shrieks even louder, while blood splats all over her" );
				Events.Add( 33792000, "Girl 2's shrieking, stops" );
				Events.Add( 34048000, "Girl 2 looks at guy 2's torn flesh/blood on her body" );
				Events.Add( 34186240, "Clip of guy 2's dismembered arm + ringing deafness sound starts" );
				Events.Add( 34560000, "Girl 2 drops down on her knees + thumping noise" );
				Events.Add( 35072000, "Girl 2 whimps and shivers" );
				Events.Add( 31616000, "Girl 2 starts screaming" );
				Events.Add( 36029440, "Cut to fence with signpost Restricted area + distant screaming" );
				Events.Add( 36193280, "Explosion in the background + mushroom cloud / Screaming stops / Limbs are flung in the air" );
				Events.Add( 36997120, "Text fades in This is what happens" );
				Events.Add( 37191680, "Text fades in when you slack off" );
				Events.Add( 37795840, "Cut to logo (dark screen) Stay in school + subtle sound of waves" );
				Events.Add( 38277120, "Cut to black" );
				//Movie 3 - Beach ends
				Events.Add( 39557120, "Cut to video" );
				Events.Add( 39680000, "Sound of a river + speaker starts speaking" );
				Events.Add( 40376320, "Man in orange overalls comes running in, while screaming" );
				Events.Add( 40811520, "Man jumps on the bear, that starts to roar" );
				Events.Add( 40929280, "Man falls to the ground" );
				Events.Add( 41344000, "Man and bear starts to box" );
				Events.Add( 41477120, "Man hits bear in stomach + punch sound" );
				Events.Add( 41728000, "Bears roars" );
				Events.Add( 41856000, "Bears starts to shuffle + man makes kungfu sounds" );
				Events.Add( 42004480, "Bear growls, and kicks man in stomach" );
				Events.Add( 42219520, "Bear kicks man on his leg" );
				Events.Add( 42368000, "Man says Oh look, an eagle, while pointing into the air" );
				Events.Add( 42511360, "Bear looks up in the air" );
				Events.Add( 42629120, "Man kicks bear in the balls + kick to the nuts sound" );
				Events.Add( 42752000, "Bear cries out in pain, while man picks up the salmon" );
				Events.Add( 42951680, "Cut to logo + speaker starts speaking again" );
				Events.Add( 43427840, "Cut to black" );
				//Movie 4 - Bear Fight ends
				Events.Add( 44707840, "Cut to video" );
				Events.Add( 44672000, "Music starts" );
				Events.Add( 46208000, "Singer starts singing" );
				Events.Add( 47308800, "Dog looking sad, over man's shoulder" );
				Events.Add( 47918080, "Subtle rain & thunder" );
				Events.Add( 48896000, "Car door closed" );
				Events.Add( 49024000, "Horse whinnying (vrinsker)" );
				Events.Add( 49280000, "Puppy barks once" );
				Events.Add( 49664000, "Music intensifies / Horse jumps out of its pen" );
				Events.Add( 50048000, "Horse whinnying (vrinsker)" );
				Events.Add( 50437120, "Man brakes his car" );
				Events.Add( 50549760, "Horses surrounds car" );
				Events.Add( 50816000, "Horse whinnying (vrinsker)" );
				Events.Add( 51020800, "Close-up of puppy returning home" );
				Events.Add( 51456000, "Puppy barks happily" );
				Events.Add( 51712000, "Horse whinnying (vrinsker)" );
				Events.Add( 52075520, "Cut to red screen (logo)" );
				Events.Add( 52387840, "Cut to black / music stops" );
				//Movie 5 - Puppy ends
				Events.Add( 53667840, "Cut to video / Guy starts to talk" );
				Events.Add( 54144000, "Guy sighs" );
				Events.Add( 54528000, "South Park" );
				Events.Add( 55040000, "Does not have souls / Nods his head and adding pressure on souls" );
				Events.Add( 55552000, "Looks away, then back / We do have souls" );
				Events.Add( 55936000, "Looks away / Lately I've been called a fat ginger/ Looks back" );
				Events.Add( 56704000, "Sighs, then whipes his nose" );
				Events.Add( 56832000, "Sniffs" );
				Events.Add( 57216000, "Raises his forehead" );
				Events.Add( 57856000, "Looks away, raises his forehead, then sighs" );
				Events.Add( 57984000, "Yells Gingers have souls!" );
				Events.Add( 58275840, "Cuts to black" );
				//Movie 6 - Redhead ends
				Events.Add( 59555840, "Fade to video + guy in grey shirt (guy 1) starts to speak: Kaere 15-aarige mig + piano starts playing" );
				Events.Add( 59904000, "Cut to woman with long dark hair (woman 1), who says: Kaere mig.." );
				Events.Add( 60037120, "Cut to young woman with blond hair and black top (woman 2), who says: paa 15" );
				Events.Add( 60154880, "Cut to woman with long red curly hair (woman 3), who says: 15" );
				Events.Add( 60231680, "Cut to bald man (guy 2), who says: Kaere mig, 15 aar" );
				Events.Add( 60436480, "Fade to black" );
				Events.Add( 60446720, "Fade to young boy with brown hair, who is smiling + woman speakover 3: Du skal ikke tro paa at skulderpuder..." );
				Events.Add( 60579840, "Cut to woman 3" );
				Events.Add( 60723200, "Cut to close up of woman with a pearl earing (woman 4), saying: Ik' smart" );
				Events.Add( 60840960, "Cut to woung woman with brown hair (woman 5), saying: Tag nu de piercinger af.." );
				Events.Add( 61004800, "Cut to woman 3, who shakes head, while saying: Det holder bare ikke.." );
				Events.Add( 61153280, "Cut to woman 4, who says: Lad nu vaer" );
				Events.Add( 61245440, "Cut to guy 2, who says Lad nu vaer" );
				Events.Add( 61312000, "Cut to guy 1, who says: Lad nu vaer" );
				Events.Add( 61404160, "Cut to woman 1, who says: Med at goere noget som JEG ikke ville ha' gjort" );
				Events.Add( 61639680, "Cut to woman 5, who smiles with closed eyes, while tilting her head to the left + woman 4 starts to speak: Lad nu vaer'.." );
				Events.Add( 61752320, "Cut to woman 4, who says: Med at sove i timerne.., while blinking and tilting her head a tiny bit" );
				Events.Add( 61972480, "Cut to guy 2, who says: Det koster.." );
				Events.Add( 62080000, "Guy 2 blinks once, then smiles" );
				Events.Add( 62336000, "Woman 2 speakover: Lad nu vaer' med at tud" );
				Events.Add( 62458880, "Cut to woman 2: over ham fra niende (9.).." );
				Events.Add( 62592000, "Woman 2 blinks once" );
				Events.Add( 62720000, "Woman 2 shakes her head, and her voice lowers a little in pitch: saa har han ikke fortjent dig.." );
				Events.Add( 63006720, "Fade to close up of a scar on a turning arm, while light dims up" );
				Events.Add( 63104000, "Woman 3 speakover: Det er det her, du skal leve med.." );
				Events.Add( 63226880, "Cut to guy 1, from another angle, saying: Resten af dit liv.." );
				Events.Add( 63431680, "Guy 1 starts to pull up his shirt" );
				Events.Add( 63467520, "Cut to scar on guy 1's back" );
				Events.Add( 63846400, "Cut to guy 2, saying: Resten af dit liv.., stretches out his collar" );
				Events.Add( 64010240, "Cut to closeup of guy 2's collar/neck scar" );
				Events.Add( 64256000, "Fade to semi-black" );
				Events.Add( 64399360, "Cut to woman 2 (blond girl), moving her hair from her neck" );
				Events.Add( 64522240, "Camera pans down, to expose woman 2's scar on her back + speakover Selvom det maaske ikke bliver saa langt.." );
				Events.Add( 64931840, "Cut to guy 2, saying: Livet, altsaa" );
				Events.Add( 65095680, "Guy 2 blinks twice times, while swallowing (it looks like)" );
				Events.Add( 65280000, "Cut to woman 2, who's looking down to the left, looking thoughtful" );
				Events.Add( 65474560, "Cut fade in from black, to woman 3 (curly red hair), who says: Kaere mig, 15 aar" );
				Events.Add( 65771520, "Cut to woman 2, saying: Du har travlt med at vaere fuld af liv + piano play intensifies (gets a little louder)" );
				Events.Add( 66012160, "Cut to woman 5, saying: Og det er lige som det skal vaere.., while shaking her head gently, blinking 2 times rapidly" );
				Events.Add( 66257920, "Cut to woman 1, saying: ligesom det skal vaere.." );
				Events.Add( 66380800, "Cut to guy 1 (looking kind of sad) saying: Som det skal vaere.." );
				Events.Add( 66560000, "Woman 1 speakover + crossfade to woman 1: Men jeg vil gerne ha' 2 sek. pause at du i rigtig mange aar, bliver ved med, at vaere fuld af liv" );
				Events.Add( 67374080, "Cut to woman 2, saying: Fuld af liv" );
				Events.Add( 67512320, "Cut to young boy, saying: Fuld af liv, with a subtle smile" );
				Events.Add( 67722240, "Cut to woman 2 closeup, half her face is shaded, she looks intensely into the camera, while woman 4 speakover: Skygge.." );
				Events.Add( 67840000, "Cut to woman 4, saying: Toej..og solcreme.." );
				Events.Add( 68090880, "Cut to woman 2, saying: Ja, det er skide irriterende.." );
				Events.Add( 68346880, "Cut to woman 4, saying: Men din hud, glemmer ikke.." );
				Events.Add( 68541440, "Cut to guy 2, saying; Og den kan slaa dig ihjel" );
				Events.Add( 68700160, "Cut to woman 4, saying: Inden du fylder 30.." );
				Events.Add( 68864000, "Cut to guy 2, saying: 45" );
				Events.Add( 68945920, "Cut to woman 2, saying: 25" );
				Events.Add( 69150720, "Cut to sideview of woman 4, saying: Kaere mig, paa 15, then a short pause (about 1 second)" );
				Events.Add( 69391360, "Cut to woman 1, holding up a black/white picture + woman 1 speakover: Her er din storesoester.." );
				Events.Add( 69642240, "Cut to woman 1, saying: Og hende vil du savne, for altid, while wrinkling her forehead" );
				Events.Add( 69888000, "Woman 1 bites her lips, then looks to the lift, while blinking heavily (about to cry)" );
				Events.Add( 70016000, "Woman 1 speakover: Hun faar diagnosen da hun fylder 36.." );
				Events.Add( 70374400, "Cut to sideview of woman 4 (earring woman)" );
				Events.Add( 70400000, "Woman 1 speakover: Og taber kampen, da hun.. + music intensifies (drumbeat on kampen)" );
				Events.Add( 70656000, "Cut to woman 1, saying: er 37 + subtle sniffing" );
				Events.Add( 70784000, "Cut to woman 3 (red hair), looking downward, being very sad" );
				Events.Add( 71040000, "Cut to woman 2 (blond hair), swallows once, then looks down quickly + woman 1 speakover: Jeg vil ha' at du skal vide det, for det er modermaerkekraeft" );
				Events.Add( 71296000, "Cut to closeup of woman 4(earring) looking directly into the camera, then blinks once, then looks down quickly + woman 1 speakover: der tager din soester" );
				Events.Add( 71552000, "Cut to woman 2, still looking down, who then puts a hand up to her face, and then starts to cry (no sound)" );
				Events.Add( 71680000, "Cut to woman 3, raising her forehead, saying: Jeg proever ikke at skraemme dig.. jeg proever at goere dig opmaerksom.., then blinks once" );
				Events.Add( 72064000, "Fade in to woman 2, saying: Hvis et nyt modermaerke dukker op.." );
				Events.Add( 72320000, "Cut to woman 5, saying: Eller et andet begynder at aendre farve.." );
				Events.Add( 72576000, "Cut to guy 2, saying: Eller stoerrelse, eller form.." );
				Events.Add( 72960000, "Cut to woman 2 (blond hair), saying: Eller noget foeles anderledes" );
				Events.Add( 73088000, "Woman 3 speakover: Saa hils din laege.." );
				Events.Add( 73216000, "Cut to closeup of woman 3, saying fra dig selv, som lidt aeldre" );
				Events.Add( 73600000, "Woman 1 speakover: Og sig.." );
				Events.Add( 73728000, "Cut to woman 1, saying at du vil undersoeges..hurtigst muligt" );
				Events.Add( 73984000, "Cut to sideview of woman 2, who swallows once + young boy speakover: Kaere moster.." );
				Events.Add( 74112000, "Cut to a pair of hands, with golden bracelets placed on a knee + young boy speakover: da du var 15.." );
				Events.Add( 74240000, "Fade in from black to young boy" );
				Events.Add( 74368000, "Young boy says: Den her film..den var til dig.." );
				Events.Add( 74752000, "Fade/blur out to white + music intensifies (drum beat on start of blurring/fading)" );
				Events.Add( 74880000, "Cut to logo + young boy speakover: Jeg ville oenske, du havde set den" );
				Events.Add( 75520000, "Cut to black" );

			}
			else if( track == 5 )
			{
				Events.Add( 3891200, "Fade to video + guy in grey shirt (guy 1) starts to speak: Kaere 15-aarige mig + piano starts playing" );
				Events.Add( 4193280, "Cut to woman with long dark hair (woman 1), who says: Kaere mig.." );
				Events.Add( 4326400, "Cut to young woman with blond hair and black top (woman 2), who says: paa 15" );
				Events.Add( 4444160, "Cut to woman with long red curly hair (woman 3), who says: 15" );
				Events.Add( 4520960, "Cut to bald man (guy 2), who says: Kaere mig, 15 aar" );
				Events.Add( 4669440, "Fade to black" );
				Events.Add( 4730880, "Fade to young boy with brown hair, who is smiling + woman speakover 3: Du skal ikke tro paa at skulderpuder..." );
				Events.Add( 4869120, "Cut to woman 3" );
				Events.Add( 5012480, "Cut to close up of woman with a pearl earing (woman 4), saying: Ik' smart" );
				Events.Add( 5130240, "Cut to woung woman with brown hair (woman 5), saying: Tag nu de piercinger af.." );
				Events.Add( 5294080, "Cut to woman 3, who shakes head, while saying: Det holder bare ikke.." );
				Events.Add( 5442560, "Cut to woman 4, who says: Lad nu vaer'" );
				Events.Add( 5534720, "Cut to guy 2, who says Lad nu vaer" );
				Events.Add( 5601280, "Cut to guy 1, who says: Lad nu vaer'" );
				Events.Add( 5693440, "Cut to woman 1, who says: Med at goere noget som JEG ikke ville ha' gjort" );
				Events.Add( 5836320, "Cut to woman 5, who smiles with closed eyes, while tilting her head to the left + woman 4 starts to speak: Lad nu vaer'.." );
				Events.Add( 6041600, "Cut to woman 4, who says: Med at sove i timerne.., while blinking and tilting her head a tiny bit" );
				Events.Add( 6261760, "Cut to guy 2, who says: Det koster.." );
				Events.Add( 6410240, "Guy 2 blinks once, then smiles" );
				Events.Add( 6666240, "Woman 2 speakover: Lad nu vaer' med at tud" );
				Events.Add( 6748160, "Cut to woman 2: over ham fra niende (9.).." );
				Events.Add( 6850560, "Woman 2 blinks once" );
				Events.Add( 7070720, "Woman 2 shakes her head, and her voice lowers a little in pitch: saa har han ikke fortjent dig.." );
				Events.Add( 7296000, "Cut to close up of a scar on a turning arm, while light dims up" );
				Events.Add( 7388160, "Woman 3 speakover: Det er det her, du skal leve med.." );
				Events.Add( 7516160, "Cut to guy 1, from another angle, saying: Resten af dit liv.." );
				Events.Add( 7720960, "Guy 1 starts to pull up his shirt" );
				Events.Add( 7756800, "Cut to scar on guy 1's back" );
				Events.Add( 8135680, "Cut to guy 2, saying: Resten af dit liv.., stretches out his collar" );
				Events.Add( 8299520, "Cut to closeup of guy 2's collar/neck scar" );
				Events.Add( 8478720, "Fade to semi-black" );
				Events.Add( 8688640, "Fade cut to woman 2 (blond girl), moving her hair from her neck" );

				Events.Add( 8832000, "Camera pans down, to expose woman 2's scar on her back + speakover Selvom det maaske ikke bliver saa langt.." );

				Events.Add( 9221120, "Cut to guy 2, saying: Livet, altsaa" );
				Events.Add( 9400320, "Guy 2 blinks twice times, while swallowing (it looks like)" );
				Events.Add( 9569280, "Cut to woman 2, who's looking down to the left, looking thoughtful" );
				Events.Add( 9768960, "Cut fade in from black, to woman 3 (curly red hair), who says: Kaere mig, 15 aar" );
				Events.Add( 10060800, "Cut to woman 2, saying: Du har travlt med at vaere fuld af liv + piano play intensifies (gets a little louder)" );
				Events.Add( 10301440, "Cut to woman 5, saying: Og det er lige som det skal vaere.., while shaking her head gently, blinking 2 times rapidly" );
				Events.Add( 10547200, "Cut to woman 1, saying: ligesom det skal vaere.." );
				Events.Add( 10670080, "Cut to guy 1 (looking kind of sad) saying: Som det skal vaere.." );
				Events.Add( 10961920, "Woman 1 speakover + crossfade to woman 1: Men jeg vil gerne ha' 2 sek. pause at du i rigtig mange aar, bliver ved med, at vaere fuld af liv" );
				Events.Add( 11663360, "Cut to woman 2, saying: Fuld af liv" );
				Events.Add( 11801600, "Cut to woman 2 closeup, half her face is shaded, she looks intensely into the camera, while woman 4 speakover: Skygge.." );
				Events.Add( 12011520, "Cut to woman 4, saying: Toej..og solcreme.." );
				Events.Add( 12129280, "Cut to woman 2, saying:Ja, det er skide irriterende.." );
				Events.Add( 12380160, "Cut to woman 4, saying: Men din hud, glemmer ikke.." );
				Events.Add( 12636160, "Cut to guy 2, saying; Og den kan slaa dig ihjel" );
				Events.Add( 12830720, "Cut to woman 4, saying: Inden du fylder 30.." );
				Events.Add( 12989440, "Cut to guy 2, saying: 45" );
				Events.Add( 13347328, "Cut to woman 2, saying: 25" );
				Events.Add( 13235200, "Cut to sideview of woman 4, saying: Kaere mig, paa 15, then a short pause (about 1 second)" );
				Events.Add( 13440000, "Cut to woman 1, holding up a black/white picture + woman 1 speakover: Her er din storesoester.." );
				Events.Add( 13680640, "Cut to woman 1, saying: Og hende vil du savne, for altid, while wrinkling her forehead" );
				Events.Add( 13931520, "Woman 1 bites her lips, then looks to the lift, while blinking heavily (about to cry)" );
				Events.Add( 14325760, "Woman 1 speakover: Hun faar diagnosen da hun fylder 36.." );
				Events.Add( 14336000, "Cut to sideview of woman 4 (earring woman)" );
				Events.Add( 14663680, "Woman 1 speakover: Og taber kampen, da hun.. + music intensifies (drumbeat on kampen)" );
				Events.Add( 14766080, "Cut to woman 1, saying: er 37 + subtle sniffing" );
				Events.Add( 14955520, "Cut to woman 3 (red hair), looking downward, being very sad" );
				Events.Add( 15150080, "Cut to woman 2 (blond hair), swallows once, then looks down quickly + woman 1 speakover: Jeg vil ha' at du skal vide det, for det er modermaerkekraeft" );
				Events.Add( 15395840, "Cut to closeup of woman 4(earring) looking directly into the camera, then blinks once, then looks down quickly + woman 1 speakover: der tager din soester" );
				Events.Add( 15646720, "Cut to woman 2, still looking down, who then puts a hand up to her face, and then starts to cry (no sound)" );
				Events.Add( 15851520, "Cut to woman 3, raising her forehead, saying: Jeg proever ikke at skraemme dig.. jeg proever at goere dig opmaerksom.., then blinks once" );
				Events.Add( 15984640, "Fade in to woman 2, saying: Hvis et nyt modermaerke dukker op.." );
				Events.Add( 16445440, "Cut to woman 5, saying: Eller et andet begynder at aendre farve.." );
				Events.Add( 16686080, "Cut to guy 2, saying: Eller stoerrelse, eller form.." );
				Events.Add( 16957440, "Cut to woman 2 (blond hair), saying: Eller noget foeles anderledes" );
				Events.Add( 17162240, "Woman 3 speakover: Saa hils din laege.." );
				Events.Add( 17566720, "Cut to closeup of woman 3, saying fra dig selv, som lidt aeldre" );
				Events.Add( 17914880, "Woman 1 speakover: Og sig.." );
				Events.Add( 17981440, "Cut to woman 1, saying at du vil undersoeges..hurtigst muligt" );
				Events.Add( 18263040, "Cut to sideview of woman 2, who swallows once + young boy speakover: Kaere moster.." );
				Events.Add( 18447360, "Cut to a pair of hands, with golden bracelets placed on a knee + young boy speakover: da du var 15.." );
				Events.Add( 18570240, "Fade in from black to young boy" );
				Events.Add( 18769920, "Young boy says: Den her film..den var til dig.." );
				Events.Add( 19072000, "Fade/blur out to white + music intensifies (drum beat on start of blurring/fading)" );
				Events.Add( 19112960, "Cut to logo + young boy speakover: Jeg ville oenske, du havde set den" );
				Events.Add( 19768320, "Cut to black" );
				// Movie 1 - Skin Cancer ends
				Events.Add( 21125120, "Cut to video" );
				Events.Add( 21125121, "Music starts" );
				Events.Add( 22830080, "Singer starts singing" );
				Events.Add( 23726080, "Dog looking sad, over man's shoulder" );
				Events.Add( 24335360, "Subtle rain & thunder" );
				Events.Add( 25512960, "Car door closed" );
				Events.Add( 25528320, "Horse whinnying (vrinsker)" );
				Events.Add( 25825280, "Puppy barks once" );
				Events.Add( 26214400, "Music intensifies / Horse jumps out of its pen" );
				Events.Add( 26470400, "Horse whinnying (vrinsker)" );
				Events.Add( 26844160, "Man brakes his car" );
				Events.Add( 26967040, "Horses surrounds car" );
				Events.Add( 27310080, "Horse whinnying (vrinsker)" );
				Events.Add( 27438080, "Close-up of puppy returning home" );
				Events.Add( 28026880, "Puppy barks happily" );
				Events.Add( 28236800, "Horse whinnying (vrinsker)" );
				Events.Add( 28492800, "Cut to red screen (logo)" );
				Events.Add( 28805120, "Cut to black / music stops" );
				//Movie 2 - Puppy ends
				Events.Add( 30085120, "Cut to video" );
				Events.Add( 30156800, "Sound of a river + speaker starts speaking" );
				Events.Add( 30929920, "Man in orange overalls comes running in, while screaming" );
				Events.Add( 31375360, "Man jumps on the bear, that starts to roar" );
				Events.Add( 31467520, "Man falls to the ground" );
				Events.Add( 31831040, "Man and bear starts to box" );
				Events.Add( 32005120, "Man hits bear in stomach + punch sound" );
				Events.Add( 32302080, "Bears roars" );
				Events.Add( 32296960, "Bears starts to shuffle + man makes kungfu sounds" );
				Events.Add( 32537600, "Bear growls, and kicks man in stomach" );
				Events.Add( 32762880, "Bear kicks man on his leg" );
				Events.Add( 32947200, "Man says Oh look, an eagle, while pointing into the air" );
				Events.Add( 33054720, "Bear looks up in the air" );
				Events.Add( 33167360, "Man kicks bear in the balls + kick to the nuts sound" );
				Events.Add( 33346560, "Bear cries out in pain, while man picks up the salmon" );
				Events.Add( 33479680, "Cut to logo + speaker starts speaking again" );
				Events.Add( 33955840, "Cut to black" );
				//Movie 3 - Bear fight ends
				Events.Add( 35235840, "Cut to warning" );
				Events.Add( 35630080, "Cut to video + engine sound" );
				Events.Add( 36352000, "A person says something uneligble" );
				Events.Add( 35998720, "A few bumping noises" );
				Events.Add( 36592640, "We see the girl" );
				Events.Add( 36894720, "The car stops, and a girl in white stands in the middle of the road" );
				Events.Add( 37422080, "Girl stands in front of camera / JUMPSCARE 1 / Passengers starts to scream" );
				Events.Add( 37657600, "Car starts to reverse / reversing sound + motor gassing up" );
				Events.Add( 38553600, "Cut to dark screen with japanese symbols + sounds fades out" );
				Events.Add( 38809600, "Girl stands with a laptop, with some uneligble text + rumbling background sound fades in" );
				Events.Add( 39608320, "Fading to white + rumbling background sound fades out" );
				Events.Add( 39746560, "Fade to driving car + a sharp, loud sound" );
				Events.Add( 40064000, "The car stops, and a girl in white stands in the middle of the road" );
				Events.Add( 40622080, "Girl stands in front of camera / JUMPSCARE 2 / Passengers starts to scream" );
				Events.Add( 40872960, "Car starts to reverse / reversing sound + motor gassing up" );
				Events.Add( 41354240, "Fade to white" );
				Events.Add( 41420800, "Fade to slowmotion driving car + slowed-down sounds (low frequency rumbling)" );
				Events.Add( 42521600, "The car stops, and a girl in white stands in the middle of the road" );
				Events.Add( 44364800, "Girl stands in front of camera / SLOW JUMPSCARE 3 / Passengers starts to scream (slow)" );
				Events.Add( 45245440, "Cut to to logo + sharp clang sound" );
				Events.Add( 45982720, "Cut to black" );
				//Movie 4 - Japanese ends
				Events.Add( 47267840, "Cut to video & Music starts" );
				Events.Add( 47979520, "Singers start singing" );
				Events.Add( 48276480, "Schoolgirl takes off her shorts" );
				Events.Add( 43059200, "Grab your friends and head out to the sun" );
				Events.Add( 49907200, "Little bell rings once" );
				Events.Add( 51998720, "2 guys toasts, with girls in background" );
				Events.Add( 52398080, "Girl 1 tumbles over guy 1, so they both lie down" );
				Events.Add( 52710400, "Girl 1 sits on top of guy 1, both looking intensively at eachother" );
				Events.Add( 52961280, "Girl 1 and guy 1 kiss eachother" );
				Events.Add( 53171200, "Mermaid-reference! (Girl 2 flings her wet hair, in the sunset(?))" );
				Events.Add( 53642240, "Girl 1 gets up from the sand" );
				Events.Add( 53795840, "Guy looks at the girl" );
				Events.Add( 53959680, "Girl 1 runs away, smiling back at guy 1" );
				Events.Add( 54144000, "Guy 1 starts to follow girl 1" );
				Events.Add( 54425600, "Girl 1 gets blown up + explosion sound" );
				Events.Add( 54492160, "Blood on the screen + splat sound" );
				Events.Add( 54696960, "Girl 2 screams" );
				Events.Add( 54737920, "Guy 1 yells Nooo!" );
				Events.Add( 54906880, "3 x beeping sound, then guy 1 explodes + (blood)explosion sound" );
				Events.Add( 54973440, "Guy 2, holding in hands with girl 2, yells Let's get outta here" );
				//Events.Add(53975040,"Girl 2 breaths heavy, while they are trying to escape");
				Events.Add( 55598080, "Guy 2 yells, at girl 2, You are slowing me down" );
				Events.Add( 55772160, "Girl 2 screams at guy 2, Shaun(possibly?)" );
				Events.Add( 55879680, "Girl 2 shrieks loudly, while guy 2 runs away" );
				Events.Add( 55976960, "Clicking noise, then guy 2 explodes" );
				Events.Add( 56069120, "Girl 2 shrieks even louder, while blood splats all over her" );
				Events.Add( 56192000, "Girl 2's shrieking, stops" );
				Events.Add( 56499200, "Girl 2 looks at guy 2's torn flesh/blood on her body" );
				Events.Add( 56616960, "Clip of guy 2's dismembered arm + ringing deafness sound starts" );
				Events.Add( 57036800, "Girl 2 drops down on her knees + thumping noise" );
				Events.Add( 57405440, "Girl 2 whimps and shivers" );
				Events.Add( 57927680, "Girl 2 starts screaming" );
				Events.Add( 58460160, "Cut to fence with signpost Restricted area + distant screaming" );
				Events.Add( 58644480, "Explosion in the background + mushroom cloud / Screaming stops / Limbs are flung in the air" );
				Events.Add( 60707840, "Text fades in This is what happens" );
				Events.Add( 59622400, "Text fades in when you slack off" );
				Events.Add( 60226560, "Cut to logo (dark screen) Stay in school + subtle sound of waves" );
				Events.Add( 60707841, "Cut to black" );
				//Movie 5 - Beach ends
				Events.Add( 61987840, "Cut to video" );
				Events.Add( 62315520, "Father does not see the business car, in the intersection + indicator sound" );
				Events.Add( 62479360, "Cut to business car perspective / Father starts to pull out on the road" );
				Events.Add( 62592000, "Business man notices the father pulls out in front of him + gasping sound" );
				Events.Add( 62766080, "Panview of business man about to collide with father + braking sound / Everything slows down to a halt" );
				Events.Add( 62832640, "Subtle humming background noise" );
				Events.Add( 63232000, "Business man exhales heavily" );
				Events.Add( 63518720, "Father breaths heavily" );
				Events.Add( 63795200, "Car door opens" );
				Events.Add( 63938560, "Business man places foot on the road + footstep sound" );
				Events.Add( 64168960, "Both walks towards eachother, father says mate..." );
				Events.Add( 64640000, "Business man says You just pulled out..." );
				Events.Add( 65029120, "Father says well, come on mate" );
				Events.Add( 65582080, "Business man says I know, if I had gone a little slower..." );
				Events.Add( 65894400, "Whoosing background noise + business car moves a little forward + tire noise" );
				Events.Add( 66042880, "Business car stops, background noise stops" );
				Events.Add( 66350080, "Father says I've got a boy in the back" );
				Events.Add( 66810880, "Cut to boy in the backseat of father's car" );
				Events.Add( 67118080, "Business man says I came too fast" );
				Events.Add( 67712000, "Business man says I'm sorry" );
				Events.Add( 67824640, "Father exhales heavily, putting his hands on his neck" );
				Events.Add( 68080640, "Cut overview: business man re-enters his car" );
				Events.Add( 68423680, "Seatbelt click" );
				Events.Add( 68459520, "Background heartbeat sound intensifies" );
				Events.Add( 68643840, "Father looks at his kid, exhaling + semi-whimpering" );
				Events.Add( 68807680, "Cut to the kid, looking at the father" );
				Events.Add( 68966400, "Seatbelt click + Time resumes" );
				Events.Add( 69053440, "Car crash into the side of father + loud crash" );
				Events.Add( 69171200, "Fade to black + sound fades out" );
				Events.Add( 69294080, "Fade to text Other people makes mistakes" );
				Events.Add( 69667840, "Cut to black" );
				//Movie 6 - Car Crash
				Events.Add( 70947840, "Cut to video / Guy starts to talk" );
				Events.Add( 71444480, "Guy sighs" );
				Events.Add( 71777280, "South Park" );
				Events.Add( 72222720, "Does not have souls / Nods his head and adding pressure on souls" );
				Events.Add( 72688640, "Looks away, then back / We do have souls" );
				Events.Add( 73077760, "Looks away / Lately I've been called a fat ginger / Looks back" );
				Events.Add( 74050560, "Sighs, then whipes his nose" );
				Events.Add( 74229760, "Sniffs" );
				Events.Add( 74593280, "Raises his forehead" );
				Events.Add( 75069440, "Looks away, raises his forehead, then sighs" );
				Events.Add( 75361280, "Yells Gingers have souls!" );
				Events.Add( 75555840, "Cuts to black" );
			}
			else
			{
				Console.WriteLine( "Wrong track................." );
			}
		}

		private void Init()
		{
			Events = new SortedDictionary<double, string>();

			foreach( Event e in eventSerializationWrapper )
			{
				if( Events.ContainsKey( e.Second ) )
				{
					e.Second += 0.001;
				}

				Events.Add( e.Second, e.Text );
			}
		}

		public IEnumerable<KeyValuePair<double, string>> GetRange( double fromInclusive, double toExclusive )
		{
			if( Events == null )
			{
				Init();
			}

			return Events.Where( kvp => kvp.Key >= fromInclusive && kvp.Key < toExclusive );
		}

		[XmlIgnore]
		public SortedDictionary<double, string> Events { get; set; }

		/// <summary>
		/// For serialization purposes only. Do not use!
		/// </summary>
		[XmlArray( "Events" )]
		public List<Event> EventSerializationWrapper
		{
			get
			{
				if( Events != null )
				{
					eventSerializationWrapper = new List<Event>();
					foreach( KeyValuePair<double, string> kvp in Events )
					{
						eventSerializationWrapper.Add( new Event( TimeSpan.FromMilliseconds( kvp.Key / 128d ).TotalSeconds - 30, kvp.Value ) );
					}
				}
				return eventSerializationWrapper;
			}
			set { eventSerializationWrapper = value; }
		}

		public class Event
		{
			public Event()
			{
			}

			public Event( double second, string text )
			{
				this.Second = second;
				this.Text = text;
			}

			public double Second { get; set; }
			public string Text { get; set; }
		}
	}
}