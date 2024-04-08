const Settings = () => {
  const handleDelete = async () => {};

  return (
    <>
      <div className="space-y-4">
        <div className="shadow p-8">
          <div className="mt-6 border-t border-gray-100">
            <dl className="divide-y divide-gray-100">
              <div className="px-4 py-6 sm:grid sm:grid-cols-3 sm:gap-4 sm:px-0">
                <dt className="text-sm text-center">
                  Permanently delete the project.
                </dt>
                <dd className=" text-sm">
                  <button className="btn btn-error">Delete</button>
                </dd>
              </div>
            </dl>
          </div>
        </div>
      </div>
    </>
  );
};

export default Settings;
