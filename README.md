# TangibleMoments Prototype

![17355244927065992](https://github.com/user-attachments/assets/5eeeac93-05ff-4c31-8320-cea47fe5096a)

This is an initial prototype for TangibleMoments, a framework we proposed which enables users to embed XR memories onto physical objects. 
It works on the Meta Quest 3 headset, and the Meta Quest 3s (untested).
The prototype scans a QR code sticker which can be placed on any physical object, then displays an option to enter an XR memory (in this case, a LiDAR scan of a room). Upon pressing the *Immerse!* button, users are immersed in the XR memory.

## Demo video
[View demo video]([https://drive.google.com/file/d/1Cjmj2VF8y7oyseL43Zvd-WS8ykAxJqeq/view?usp=sharing](https://drive.google.com/file/d/1o5lQPipK9xD9BnOwcPuS56LY1ME5SIUf/view)).

## APK download
You can demo this prototype on your own Quest 3 or Quest 3s headset: [Download APK](https://drive.google.com/file/d/1Cjmj2VF8y7oyseL43Zvd-WS8ykAxJqeq/view?usp=sharing).

## Usage
Upon launching the application and allowing it to access your display, look at the QR code provided below through the headset.

<p align="center">
<img style="width:300px" src="https://github.com/user-attachments/assets/242cdb9c-cfab-44c4-ba83-7ce05830a720"/>
</p>

The application will recognize this QR code and display a preview of the XR memory associated with it. Touch the *Immerse!* button with your controller to enter VR and experience the memory with 6 degrees of freedom.

## Future directions

- Currently, this prototype is limited only to *Recalling Moments*. We have yet to implement the remaining processes we described as part of the TangibleMoments framework: *Creating Moments*, *Sharing Moments*, *Copying Moments*, and *Clearing Moments*.
- At the time of developing this, Meta has not yet released camera API access to Quest developers. We are using the Android display API as a workaround. This causes the QR code to be unreadable if there is virtual content covering it. Camera API access should be used once it is available to Quest developers.
- Since this is a proof-of-concept, we are currently using QR codes to link memories to physical objects. In the future, custom computer vision models will be used to recognize objects which are associated with XR memories, without QR codes needing to be involved.
- We have not yet provided a way to exit out of the XR memory once immersed.
