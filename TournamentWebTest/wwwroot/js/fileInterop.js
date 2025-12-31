window.downloadFile = function (filename, contentType, base64Data) {
    const link = document.createElement('a');
    link.download = filename;
    link.href = `data:${contentType};base64,${base64Data}`;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
};

window.saveWithPicker = async function (suggestedName, contentType, textContent) {
    if (!window.showSaveFilePicker) {
        return { ok: false, reason: "no-picker" };
    }

    const handle = await window.showSaveFilePicker({
        suggestedName,
        types: [{
            description: contentType,
            accept: { [contentType]: [".json"] }
        }]
    });

    const writable = await handle.createWritable();
    await writable.write(new Blob([textContent], { type: contentType }));
    await writable.close();

    return { ok: true };
};
