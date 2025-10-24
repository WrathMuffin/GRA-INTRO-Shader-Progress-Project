# ConflictSolver


Diffuse Lighting:
<img width="882" height="435" alt="image" src="https://github.com/user-attachments/assets/782d02f1-1b48-4be7-ad6c-dff05117504d" />
Diffuse lighting is the simplest form of lighting that uses the main light direction of the unity scene, and the normals of the object vectors. These two vectors are normalized and used in the dot product function to make a lighting value, and then use the saturate function to restrict the range, and then added it to the Base Color function of the Fragment shader. The shadows are pitch black as the lighting is only concerned with the single light source (main scene light) affecting the object, so its artifical lighting. Unfortuantly to mantain the style of our game being retro themed, we didnt find a place to use this kind of lighting.

Diffuse-Ambient:
<img width="870" height="408" alt="image" src="https://github.com/user-attachments/assets/34fbe121-0c4c-4953-b666-b3590af10cb9" />
Diffuse Ambient is very similar to diffuse lighting, in that the process is almost the same with needing the main light and normal vectors being normalized. Though the key difference is the shadow color itself, it is not pitch black this time around but instead the shaded areas are toned down in brightness, as this lighting tries to simulate the room ambient lighting and shadows of the environment. We also couldnt find a use for it in our game unfortuantly as it conflicts with our artistic vision for it.

Toon Shader:
<img width="918" height="533" alt="image" src="https://github.com/user-attachments/assets/40524b23-4c3c-453e-92d5-8ca89db1c18b" />
The Toon shader renders 3D objects to have a more cartoony appearence by applying a toon ramp texture over the object with the uv involved to simulate cell shading. The normals and the main light direction are normalized and used in the dot product and multiplied by negative one to have the lighting be calculated and corrected, and then that is hooked to the saturate function and connected to the UV port of the Sample Texture 2D node. What this does is that it modifies the UV of the object to map out the toon ramp texture (that property is plugged into the Sample Texture 2D node) to give that cell shading look depending on where the main lighting is coming from. Then that gets added to the BaseColor node of the Fragment shader to be applied and work in game. 

With Toon Shader (look more closely at the object, you can find the outline of the highlights of the toon ramp):
<img width="897" height="550" alt="image" src="https://github.com/user-attachments/assets/14303b2f-5291-4f0d-90b8-b59d4fc2be67" />

Without Toon Shader:
<img width="901" height="575" alt="image" src="https://github.com/user-attachments/assets/8ad04f4f-4c81-4bbe-a2e5-3be2aa981452" />

The Toon Shader is used for every boss, and that is so to help give more of a retro and poly feel of the boss characters with how lighting is displayed on them instead of giving more smooth gradients for lighting and shading.

Bump Shader:
<img width="1152" height="523" alt="image" src="https://github.com/user-attachments/assets/c0d61e79-c626-4d51-96dc-2bca33bcf17d" />
The bump shader makes the texture applied to the object have more depth to it without modifying the geometry of the mesh itself. This was done through recalculating the normals of the mesh. A normal map needs to be sampled and then unpacked first, and using the rbg values the X, Y, and Z coordinates of the normal vectors along with needing to convert Tangent, Bitangent, and the object normals into world space and multiply them together into a big 3 by 3 matrix to be used in the dot product for lighting calculations with the Main Light Direction (also being normalized). The dot product is multiplied by -1 to flip it around to correct it, and saturated and multiplied with the main object textured that is sampled so that the depth effect can be applied to it, and be added to the BaseColor node in the Fragment shader to use the effect. 

Rim Lighting: 
<img width="723" height="557" alt="image" src="https://github.com/user-attachments/assets/d6c72d65-7bdf-4d19-970e-dc356a94056b" />

The rim lighting was accomplished by first, getting both the normal vectors of the object (in world space), and the view direction (in world space), and getting them both normalized. From there a dot product is found between them to get the lighting value, and then it is run through the saturation node so that the results are restricted between 0 and 1 (no negative numbers for lighting calculations). Then it is subtracting against 1 so that the lighting does not come from the center of the object but at the edges of it. 

<img width="440" height="587" alt="image" src="https://github.com/user-attachments/assets/39a061d4-daec-4615-b1a6-ce5b3720c0ad" />

Then the rim lighting effect is further restricted by the power node using the lighting calculation difference with the Rim power property so that the rim lighting can be further tightened to the object edges. There is also the Rim color and an additinoal newly added Rim glow property, where the Rim color dictates the color of the rim lighting by multiplying the lighting result with the color values of the rim color, and the Rim glow multiplies with that result to allow us to adjust how much glow the objetc will glow for the rim lighting. Then finally that result is added to the Base Color node of the fragment shader to apply it to the object in game. 

<img width="135" height="167" alt="image" src="https://github.com/user-attachments/assets/7a931cb9-c74c-4804-91a4-66a1d85404d6" />
<img width="496" height="427" alt="image" src="https://github.com/user-attachments/assets/6388e6cf-6458-4fcf-8794-ad3225684804" />

Rim lighting was used the majority of the time in this game, as most of it takes place in a technology and sci fi inspired world. So, to help convey that the player character and the environment props like the platform objects in our game use rim lighting to make things glow. For the player the inside of the coat and rim around the fedora glows blue to convey a sense of "good" as opposed to the red "evil" for the enemies, as the surfaces emit the rim glow effect due to how the lighting is calculated for more angular objects instead of spherical. As for the platforms parts of them glow green as the background of the game is literally falling dark green texts of code so having more green lighting will match with the environment in place. 

Reflection shader: 

<img width="887" height="742" alt="image" src="https://github.com/user-attachments/assets/468ef31d-04f9-4b59-a730-e874fae87efa" />
<img width="812" height="342" alt="image" src="https://github.com/user-attachments/assets/29521cbd-d4d2-458d-b065-a0654896b025" />

The reflection shader uses a cubemap that is projected onto the object from the light reflecting off from the object. Firstly, the normal vectors and the view direction in world space are normalized, but the view direction is multiply by -1 so that the light reflects away from the object. These nodes are then connected to the Sample Reflected Cubemap node, which has the cubemap property connected (so that any cubemap can be sampled and reflected) along with the view direction and normal vectors as well with setting the sampler to have the filter be linear so it can work properly. The Reflection intensity property is multiplied with the result of the Sample Reflected Cubemap node, along with the Reflection blend property so that how much of the cube map is being projected and mixed with the texture of the object is used, and that gets also added to the Base Color node of the fragment shader to display the cube map reflection on the object surfaces. 

<img width="1488" height="744" alt="eree" src="https://github.com/user-attachments/assets/1a2b9f86-7617-420d-8363-72ba0a416183" />

The above image shows the enemy (on the right) reflecting the cubemap off the polygonal skin.

The reflection shader is used to further enhance the sci-fi environment, as the game literally takes place within the programming of the repository software Bithub (which is the main antagonist of the game). Along with making things glow, having parts of the environment reflect out images of code helps better convey the idea of being in the world of software and programming, and that same cube map is the same one used in the environment of the levels in displaying a more reflective material for the platforms which adds visual interest but also a sense of realism as lighting acts in a way we expect. Additionaly, this will help convey the feeling of plastic and metal, fittingly with the theme of technology.

Specular shader:

<img width="952" height="732" alt="image" src="https://github.com/user-attachments/assets/5b46d615-efe9-4bcc-9592-d5954d6e7219" />

The specular shader is used to make objects appear more reflective by adding highlights to the surface. The main light direction vector is normalized, along with the normal vectors in world space and normalized are linked to the reflection node so that the light rays itself reflects away from the object and reacts accordingly depending on where you look. Then the reflection node is connected to the dot product node with the view direction in world space and normalized so that we can a value for the lighting, and use the saturate node to restrict the values between 0 and 1. That then gets used in the power node with the Shininess property so that the lighting effect becomes a shiny dot that can be amplified or reduced in power, and the specular is multiplied with the Specular color property so that the color values are multiplied with the lighting effect to change the color of the specular. Finally that gets added to the Base Color node in the Fragment shader so that a specular dot can appear on the object.

<img width="912" height="451" alt="image" src="https://github.com/user-attachments/assets/6b459d63-fe55-4a5b-a827-4aed710d0189" />

With Specular:

<img width="1276" height="463" alt="image" src="https://github.com/user-attachments/assets/d7334769-aae3-4fd3-935f-5457a3adb545" />

Without Specular:

<img width="652" height="421" alt="image" src="https://github.com/user-attachments/assets/ed720ae0-2676-4859-a1d2-796d79f7ad80" />

The specular effect is only used once in our game and that is for one of our boss fights, specifically the BitIgnore Capybara boss. It is subtle and faint, but if you were to look on the back of the base you can the faint big red highlight of the specular effect on the back. As to why this effect was applied like this to make the surface more smooth and slightly reflective for visual appeal of the boss itself. The BitIgnore boss is the most organic, realictic in terms of style (compared to a real capybara), so using the specular lighting let us hmake the boss looks more organic while still retains the low poly look for that still fits in with the retro theme of the game.

Flat shader:

<img width="838" height="658" alt="image" src="https://github.com/user-attachments/assets/8cb9c42f-93b8-416b-9bcd-06e67d066484" />

The flat shader changes the object's appearence to have a more low-poly render style from recalculating the normal vectors to give that appearence. The position node in world space was used for the DDX and the DDY nodes, and these two functions recalculate the normals on the horizontal and vertical axis. The DDX node recalculates the normals using the difference of a pixel being next to another pixel, and the DDY node uses the difference of a pixel being above another pixel. The position nodes were used in this way so that the individual faces of the model can be used in calculations with their normal directions. The product of the cross product function is then normalized and used in the dot product node with the main light direction vector which is normalized, so that the lighting calculations can be done with how the light will be calculated with the modified face normals. Finally, the Base color property is used to multiply the lighting effect with the color values to change how the flat shading looks in shading gradients and appearence which is then added to the Base Color node in the Fragment shader.

With flat shading:

<img width="1345" height="572" alt="image" src="https://github.com/user-attachments/assets/1042579d-342d-4aeb-b7c5-02920de0fcd3" />

Without flat shading:

<img width="1390" height="561" alt="image" src="https://github.com/user-attachments/assets/d6d55fa1-95f2-4dbb-8616-500b9a58f9c8" />

This shader was used for all of the material used, that includes the props and the characters. This was done for both giving a more low-poly appearence for the model to fit with the overall style of the game, and also control how shadows are displayed on the object. As with the flat shading there is more darker shadows to match with the environment to further emphasize the low-poly designs of our models in the game to give more of a retro look. 

Color Correction:

<img width="1076" height="772" alt="image" src="https://github.com/user-attachments/assets/23a3208b-b8c8-40c3-ade4-04a29c1105e8" />

<img width="1375" height="552" alt="image" src="https://github.com/user-attachments/assets/e248bf5d-1100-4bac-8119-b670953a9c9e" />

It changes the color and the overall mood of a scene using a Look Up Table (LUT) as a 3D texture to map out the colors in scene and replace them in the final output as a post processing effect. This effect is complex, but the gist of it is that the LUT bar property has its texture file dimensions divided by 0.5 (this is the texel offsets). Then in the subtract node 32 is subtracted by 1, which is the stand in for the 32-bit COLORS variable and then divided by 32 (what would be the COLORS variable) so that data along with the offsets can be used to calculate the coordinates using the LUT to map out the colors within the threshhold from the LUT. Once the coordinates are calculated in the LUT table to map out the colors, the LUT is sampled for the color graded pixels and outputed in the Fragment shader.

The shader works by using the 2D LUT texture as a 3D cube (think of a Rubik's cube) for color correction. Each piexl is remapped using this LUT cube to change the hue and tone similar to a filter. The split node first separates the input texture into RGB and Alpha. To set up the LUT, the calculation needs to take into the account that the LUT is a flatted 3D cube with 32^3 dimension, so the size is the LUT's resolution. To get the correct UV map, we need to somehow convert rgb into uv: 

- We need to split the LIT into slices by using the Blue channel, which will decides which row to sample from
- For u, we need to use the Red channel since U axis corresponds across the Red channel and the LUT slices
- For v, we'll use the Green channel, since it ran across the Green channel

In the end we have u, and v for the LUT coordinates. Use it as the UV map for the sample texture 2D and it will outpout the remapped RGB values. For the lerp node, this essentially controls how much the color correction will affect the scene, with 1 being the max and the most affected, to 0 which is the original color of the scene.

Color correction was used in our project to enhance major events, essentially evoking feelings using tones. 

Nostalgic LUT:

<img width="1024" height="32" alt="nostalgicLUT" src="https://github.com/user-attachments/assets/1be2318a-b12d-44c8-8b12-63114212d131" />

The Nostalgic LUT is, well, nostalgic! It evokes the sense of nostalgia by giving the scene a light pastel theme, basically evoking a retro game look (inpired by GameBoy's theme). This LUT is used for the introduction of the game only, which starts out as a pixel 2D styled game, fittingly wit hthe GameBoy inspired themes. Additionally, the nostalgic LUT also gives us high contrast, which makes the objects in the scene more eye catching and sharp.

The scene before nostalgic LUT:

<img width="1369" height="762" alt="og" src="https://github.com/user-attachments/assets/68271716-5e38-42b6-ad2f-5c3f1f6a80a2" />

After nostalgic LUT is applied:

<img width="1368" height="764" alt="with nostalgicLUT" src="https://github.com/user-attachments/assets/3b164c0d-814b-427e-aa68-d29d7c2d8257" />

Red ominous LUT:

<img width="1024" height="32" alt="red" src="https://github.com/user-attachments/assets/88d9761b-490a-4f72-a480-b0fdb20a3a01" />

The Red LUT is made to be more menacing with the use of purely the color red as its more associated with danger, and that is used for the feeling of tension and dread for when the main character dies to make the death more impactful for the player, even if only briefly.

The scene before red ominous LUT:

<img width="1706" height="955" alt="Screenshot 2025-10-24 at 8 17 17 AM" src="https://github.com/user-attachments/assets/c37d496d-51f4-48f3-9806-ca98ba1524b4" />

After red ominous LUT is applied:

<img width="1705" height="961" alt="Screenshot 2025-10-24 at 8 17 34 AM" src="https://github.com/user-attachments/assets/c75e1a60-6c87-4913-929f-2fa18dd91e60" />

Solarize LUT:

<img width="1024" height="32" alt="SolarizeLUT" src="https://github.com/user-attachments/assets/d4cc72f8-08f4-42b7-bf5a-1732757fd3a6" />

The solarizing LUT does what it says in the name; it creates a warped effect by reversing the tone of the image. In the game, this is used to give impact to certain actions. Namely, landing the hit on a boss will use this effect and slow down the game to enhance the impact of the final blow.

The scene before solarize LUT:

<img width="1706" height="955" alt="Screenshot 2025-10-24 at 8 17 17 AM" src="https://github.com/user-attachments/assets/356f2699-4428-46dd-9839-9bf24d68ce88" />

After solarize LUT is applied:

<img width="1710" height="961" alt="Screenshot 2025-10-24 at 8 17 01 AM" src="https://github.com/user-attachments/assets/3504d12d-e97b-4375-a5e7-4b36b7bda243" />
