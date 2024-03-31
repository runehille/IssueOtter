import { useLocation } from "react-router";

const Breadcrumbs = () => {
  const location = useLocation();
  const paths = location.pathname
    .split("/")
    .filter((path) => path !== "app" || "");

  return (
    <div className="text-sm breadcrumbs">
      <ul>
        {paths.map((filteredPaths, index) => (
          <li key={index}>{filteredPaths}</li>
        ))}
      </ul>
    </div>
  );
};

export default Breadcrumbs;
