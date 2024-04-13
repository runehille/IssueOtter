const IssuesListSkeleton = () => {
  return (
    <div className="flex space-x-10">
      <div className="flex flex-col gap-4">
        <div className="skeleton h-6 w-24"></div>
        <span></span>
        <span></span>
        <div className="skeleton h-4 w-80"></div>
        <div className="skeleton h-4 w-80"></div>
        <div className="skeleton h-4 w-80"></div>
      </div>
      <div className="flex flex-col gap-4">
        <div className="skeleton h-64 w-72"></div>
        <div className="flex flex-col gap-2">
          <div className="skeleton h-2 w-32"></div>
          <div className="skeleton h-2 w-32"></div>
        </div>
      </div>
    </div>
  );
};

export default IssuesListSkeleton;
