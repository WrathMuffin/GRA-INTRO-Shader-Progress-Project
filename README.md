# ConflictSolver

Rim Lighting: 

<img width="723" height="557" alt="image" src="https://github.com/user-attachments/assets/d6c72d65-7bdf-4d19-970e-dc356a94056b" />
The rim lighting was accomplished by first, getting both the normal vectors of the object (in world space), and the view direction (in world space), and getting them both normalized. From there a dot product is found between them to get the lighting value, and then it is run through the saturation node so that the results are restricted between 0 and 1 (no negative numbers for lighting calculations). Then it is subtracting against 1 so that the lighting does not come from the center of the object but at the edges of it. 
<img width="440" height="587" alt="image" src="https://github.com/user-attachments/assets/39a061d4-daec-4615-b1a6-ce5b3720c0ad" />
Then the rim lighting effect is further restricted by the power node using the lighting calculation difference with the Rim power property so that the rim lighting can be further tightened to the object edges.There is also the Rim color and Rim glow properties, where the Rim color dictates the color of the rim lighting by multiplying the lighting result with the color values of the rim color, and the Rim glow multiplies with that result to amplify the glow factor of the rim lighting. Then finally that result is added to the Base Color node of the fragment shader to apply it to the object in game. 

<img width="135" height="167" alt="image" src="https://github.com/user-attachments/assets/7a931cb9-c74c-4804-91a4-66a1d85404d6" />
<img width="496" height="427" alt="image" src="https://github.com/user-attachments/assets/6388e6cf-6458-4fcf-8794-ad3225684804" />
Rim lighting was used the majority of the time in this game, as most of it takes place in a technology and sci fi inspired world. So, to help convey that the player character and the environment props like the platform objects in our game use rim lighting to make things glow. For the player the inside of the coat and rim around the fedora glows blue, as the surfaces emit the rim glow effect due to how the lighting is calculated for more angular objects instead of spherical. As for the platforms parts of them glow green as the background of the game is literally falling dark green texts of code so having more green lighting will match with the environment in place. 

Reflection shader: 

<img width="887" height="742" alt="image" src="https://github.com/user-attachments/assets/468ef31d-04f9-4b59-a730-e874fae87efa" />
<img width="812" height="342" alt="image" src="https://github.com/user-attachments/assets/29521cbd-d4d2-458d-b065-a0654896b025" />
The reflection shader uses a cubemap that is projected onto the object from the light reflecting off from the object. Firstly, the normal vectors and the view direction in world space are normalized, but the view direction is multiply by -1 so that the light reflects away from the object. These nodes are then connected to the Sample Reflected Cubemap node, which has the cubemap property connected (so that any cubemap can be sampled and reflected) along with the view direction and normal vectors as well with setting the sampler to have the filter be linear so it can work properly. The Reflection intensity property is multiplied with the result of the Sample Reflected Cubemap node, along with the Reflection blend property so that how much of the cube map is being projected and mixed with the texture of the object is used, and that gets also added to the Base Color node of the fragment shader to display the cube map reflection on the object surfaces. 

<img width="1045" height="440" alt="image" src="https://github.com/user-attachments/assets/ad8b3e24-4e5f-4db6-b70d-2d6d26e7cd2c" />
The reflection shader is used to further enhance the sci-fi environment, as the game literally takes place within the programming of the repository software Bithub (which is the main antagonist of the game). Along with making things glow, having parts of the environment reflect out images of code helps better convey the idea of being in the world of software and programming, and that same cube map is the same one used in the environment of the levels in displaying a more reflective material for the platforms which adds visual interest but also a sense of realism as lighting acts in a way we expect.

Specular shader:

<img width="952" height="732" alt="image" src="https://github.com/user-attachments/assets/5b46d615-efe9-4bcc-9592-d5954d6e7219" />
The specular shader is used to make objects appear more reflective by adding highlights to the surface. The main light direction vector is normalized, along with the normal vectors in world space and normalized are linked to the reflection node so that the light rays itself reflects away from the object and reacts accordingly depending on where you look. Then the reflection node is connected to the dot product node with the view direction in world space and normalized so that we can a value for the lighting, and use the saturate node to restrict the values between 0 and 1. That then gets used in the power node with the Shininess property so that now ithe lighting effect becomes a shiny dot that can be amplified or reduced in power, and the specular is multiplied with the Specular color property so that the color values are multiplied with the lighting effect to change the color of the specular. Finally that gets added to the Base Color node in the Fragment shader so that a specular dot can appear on the object.
<img width="912" height="451" alt="image" src="https://github.com/user-attachments/assets/6b459d63-fe55-4a5b-a827-4aed710d0189" />

With Specular:

<img width="1276" height="463" alt="image" src="https://github.com/user-attachments/assets/d7334769-aae3-4fd3-935f-5457a3adb545" />
Without Specular:
<img width="652" height="421" alt="image" src="https://github.com/user-attachments/assets/ed720ae0-2676-4859-a1d2-796d79f7ad80" />

The specular effect is only used one in our game and that is for one of our boss fights, specifically the BitIgnore Capybara boss. It is subtle and faint, but if you were to look on the back of the base you can the faint big red highlight of the specular effect on the back. As to why this effect was applied like this to make the surface more smooth and slightly reflective for visual appeal of the boss itself, with using both the jagged and angular edges and surfaces with a sense of smoothness and organic shape in general compared to the most of the bosses in general.

Flat shader:

<img width="838" height="658" alt="image" src="https://github.com/user-attachments/assets/8cb9c42f-93b8-416b-9bcd-06e67d066484" />
The flat shader changes the object's appearence to have a more low-poly render style from recalculating the normal vectors to give that appearence. The position node in world space was used for the DDX and the DDY nodes, and these two functions recalculate the normals on the horizontal and vertical axis. The DDX node recalculates the normals using the difference of a pixel being next to another pixel, and the DDY node uses the difference of a pixel being above another pixel. The position nodes were used in this way so that the individual faces of the model can be used in calculations with their normal directions. The product of the cross product function is then normalized and used in the dot product node with the main light direction vector which is normalized, so that the lighting calculations can be done with how the light will be calculated with the modified face normals. Finally, the Base color property is used to multiply the lighting effect with the color values to change how the flat shading looks in shading gradients and appearence which is then added to the Base Color node in the Fragment shader.

With flat shading:

<img width="1345" height="572" alt="image" src="https://github.com/user-attachments/assets/1042579d-342d-4aeb-b7c5-02920de0fcd3" />
Without flat shading:
<img width="1390" height="561" alt="image" src="https://github.com/user-attachments/assets/d6d55fa1-95f2-4dbb-8616-500b9a58f9c8" />

This shader was also used only for the Capybarsa boss fight. This was done for both giving a more low-poly appearence for the boss' model to fit with the overall style of the game, and also control how shadows are displayed on the object. As with the flat shading there is more darker shadows to match with the environment to further emphasize the low-poly designs of our characters in the game to give more of a retro look. 

Color Correction:

<img width="1076" height="772" alt="image" src="https://github.com/user-attachments/assets/23a3208b-b8c8-40c3-ade4-04a29c1105e8" />
<img width="1375" height="552" alt="image" src="https://github.com/user-attachments/assets/e248bf5d-1100-4bac-8119-b670953a9c9e" />

Color correction changes the color and the overall mood of a scene using a Look Up Table (LUT) as a 3D texture to map out the colors in scene and replace them in the final output as a post processing effect. This effect is complex, but the gist of it is that the LUT bar property has its texture file dimensions divided by 0.5 (this is the texel offsets). Then in the subtract node 32 is subtracted by 1, which is the stand in for the 32-bit COLORS variable and then divided by 32 (what would be the COLORS variable) so that data along with the offsets can be used to calculate the coordinates using the LUT to map out the colors within the threshhold from the LUT. Once the coordinates are calculated in the LUT table to map out the colors, the LUT is sampled for the color graded pixels and outputed in the Fragment shader.

(INSERT COLOR CORRECTION SCREENSHOTS HERE):

Color correction was used in our project to differentiate the level environments in our game, so that every boss level and stage feels different in tone. The Red LUT table is made to be more menacing with the use of purely the color red as its more associated with danger, and that is used for the feeling of tension and dread for when the main character dies to make the death more impactful for the player, even if only briefly
