
const profileImage = document.getElementById("profileImageUrl"), preview = document.querySelector('.preview')

profileImage.addEventListener('change', function () {
    [...this.files].map(file => {
        const reader = new FileReader();
        const temparalimage = document.getElementById("userProfileImage")
        reader.addEventListener('load', function () {
            console.log(this.result);
            temparalimage.src = this.result;
        })
        reader.readAsDataURL(file);
    })
})

let imageRemovebtn = document.getElementById("btnimageRemove");
let imagePath = document.getElementById("profileImageUrl");
let profilImage2 = document.getElementById("userProfileImage");

function RemoveImage() {
    imagePath.value = null;
    //alert(imagePath);
    profilImage2.src = "https://i.imgur.com/iZhMGsd.jpg";
    $.ajax({
        method: "POST",
        url: '/User/RemoveProfilImage',
        success: function (result) {
            if (result > 0) {
                toastrSuccess("Profil fotoğrafı kaldırıldı.");
            }
            else if (result == 0) {
                toastrInfo("Profil fotoğrafına sahip değilsiniz.")
            }
            else {
                toastrFail("Profil fotoğrafı kaldırılamadı.")
            }
        }
    })
}