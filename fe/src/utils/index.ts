const showConfirm = (cb: Function, message?: string) => {
    const isConfirm = window.confirm(message || `You are about to permanently delete these items from your site. \nThis action cannot be undone. \n'Cancel' to stop, 'OK' to delete.`);

    if (isConfirm) {
        cb();
    }
};

export { showConfirm };